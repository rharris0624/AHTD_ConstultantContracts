using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ConsultantContractsInternal.Models;
using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.Utilities;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Glimpse.AspNet;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using ConsultantContractsInternal.Security.Attributes;
using System.Text.RegularExpressions;

namespace ConsultantContractsInternal.Controllers
{
    public class ConsultantsController : SecuredController
    {
        //
        // GET: /Contractors/

        public ActionResult Index()
        {
            if (Roles.Admin.Split(',').Contains(CurrentUser.Role))
                ViewBag.WriteAccess = true;
            else
                ViewBag.WriteAccess = false;

            return View();
        }

        public ActionResult Recent20(string q)
        {
            var results = Filter(q);
            return Json(results.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetById(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var c = context.Consultants.Find(id);
                return Json(new
                    {
                        TaxId = String.Format("{0}-{1}", TrimOrEmpty(c.TaxId), c.SeqNo),
                        ConId = c.ConsultantId,
                        c.Name,
                        BusinessPhoneNumber = c.BusinessPhoneNumber,
                        ContactPhoneNumber = c.ContactPhoneNumber,
                        //HOOverhead = c.HomeOfficeOverheadRateMax,
                        //FCCM = c.FCCM,
                        //FSOverhead = c.FieldServiceOverheadRateMax,
                        //Multiplier = c.Multiplier
                }
                , JsonRequestBehavior.AllowGet);
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Search(string term)
        {
            var r = Filter(term);
            return Json(r.Select(c => new
            {
                label = String.Format("{0}-{1}: {2}", TrimOrEmpty(c.TaxId), c.SeqNo, c.Name),
                value = c.ConsultantId,
                data = new
                {
                    TaxId = String.Format("{0}-{1}", TrimOrEmpty(c.TaxId), c.SeqNo),
                    ConId = c.ConsultantId,
                    c.Name,
                    HOOverhead = c.HomeOfficeOverheadRateMax,
                    FCCM = c.FCCM,
                    FSOverhead = c.FieldServiceOverheadRateMax,
                    Multiplier = c.Multiplier
                }
            }), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Search2(string term, string job)
        {
            var r = Filter(term);
            return Json(r.Select(c => new
            {
                label = String.Format("{0}-{1}: {2}", TrimOrEmpty(c.TaxId), c.SeqNo, c.Name),
                value = c.ConsultantId,
                data = new
                {
                    TaxId = String.Format("{0}-{1}", TrimOrEmpty(c.TaxId), c.SeqNo),
                    ConId = c.ConsultantId,
                    c.Name,
                    HOOverhead = c.HomeOfficeOverheadRateMax,
                    FCCM = c.FCCM,
                    FSOverhead = c.FieldServiceOverheadRateMax,
                    Multiplier = c.Multiplier
                }
            }), JsonRequestBehavior.AllowGet);
        }

        public string TrimOrEmpty(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                return s.Trim();
            }
            return String.Empty;
        }

        public ActionResult Details(int id)
        {
            using (var context = new ConsultantContractsEntities())
            {
                if (Roles.Admin.Split(',').Contains(CurrentUser.Role))
                    ViewBag.WriteAccess = true;
                else
                    ViewBag.WriteAccess = false;
                var consultant = context.Consultants.Find(id);
                if (consultant.BusinessPhoneNumber != null)
                    consultant.BusinessPhoneNumber = Regex.Replace(consultant.BusinessPhoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                if (consultant.ContactPhoneNumber != null)
                    consultant.ContactPhoneNumber = Regex.Replace(consultant.ContactPhoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                return View(consultant);
            }

        }

        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult Create(Consultant model, string statename = null)
        {
            if (!ModelState.IsValid)
                return Json(new { status = "ERROR", errors = ModelState.Select(m => new { key = m.Key, value = m.Value }).ToList() });
            try
            {

                using (var context = new ConsultantContractsEntities())
                {
                    var _checkresult = context.Consultants.Where(p => p.TaxId == model.TaxId && p.SeqNo == model.SeqNo).Select(p => p).Count();
                    if (_checkresult == 0 || (model.TaxId == null && model.SeqNo == null))
                    {
                        if (model.TaxId == null && model.SeqNo == null)
                        {
                            int? seqNo = 1;
                            var subCons = context.Consultants.Where(p => p.TaxId.Equals(""));
                            if (subCons != null && subCons.Count() > 0)
                            {
                                seqNo = subCons.Max(p => p.SeqNo);
                                seqNo = seqNo != null ? seqNo + 1 : 1;
                            }
                            model.TaxId = "";
                            model.SeqNo = seqNo;
                        }
                        else if (model.TaxId == null)
                        {
                            model.TaxId = "";
                            model.SeqNo += 1;
                        }
                        int newId;
                        if (!string.IsNullOrEmpty(statename) && statename != "Temp")
                        {
                            var stateslist = context.CountryRegionNames.Where(p => p.REGION_NAME.ToUpper() == statename.ToUpper() && p.COUNTRY_CODE == model.CountryCode).Select(s => s.REGION_CODE).FirstOrDefault();
                            model.State = stateslist;
                        }
                        Regex rgx = new Regex("[^0-9]");
                        if (model.BusinessPhoneNumber != null)
                            model.BusinessPhoneNumber = rgx.Replace(model.BusinessPhoneNumber, "");
                        if (model.ContactPhoneNumber != null)
                            model.ContactPhoneNumber = rgx.Replace(model.ContactPhoneNumber, "");
                        model.LastUpdateDate = DateTime.Now;
                        model.LastUpdateUser = AHTD.Text.CommonText.StripNTDomain(CurrentUser.WindowsAccountName);
                        context.Consultants.Add(model);
                        context.SaveChanges();
                        newId = model.ConsultantId;

                        return Json(new { status = "OK", id = newId });
                    }
                    else { 
                        return Json(new { status = "Already Exist" }); }
                }

            }
            catch (DbEntityValidationException ex)
            {
                return Json(new { status = "ERROR", error = ex.GetExceptionMessages() });
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", error = ex.GetExceptionMessages() });
            }
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult SetInactive(bool setInactive, int conId)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var con = context.Consultants.SingleOrDefault(c => c.ConsultantId == conId);

                if (con == null) return new HttpNotFoundResult();

                con.Inactive = setInactive;
                con.LastUpdateDate = DateTime.Now;
                con.LastUpdateUser = AHTD.Text.CommonText.StripNTDomain(CurrentUser.WindowsAccountName);

                context.SaveChanges();

                return Json(new { status = "OK" });
            }
        }

        [HttpGet]
        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult Edit(int id)
        {

            using (var context = new ConsultantContractsEntities())
            {
                var jsonConfig = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var _consultantList = context.Consultants.ToList().Where(p => Convert.ToInt32(p.ConsultantId) == id).Select(s => s).ToList().FirstOrDefault();
                ViewBag.consultant = _consultantList;

                return View();
            }
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.Devministrator)]
        public ActionResult Edit(Consultant model, string statename = null)
        {
            if (!ModelState.IsValid)

                return Json(new { status = "ERROR", errors = ModelState.Select(m => new { key = m.Key, value = m.Value }).ToList() });

            try
            {
                using (var context = new ConsultantContractsEntities())
                {
                    var consultant = context.Consultants.Find(model.ConsultantId);

                    Regex rgx = new Regex("[^0-9]");
                    if (model.ContactPhoneNumber != null && model.ContactPhoneNumber.Length >= 13)
                        model.ContactPhoneNumber = rgx.Replace(model.ContactPhoneNumber, "");
                    if (model.BusinessPhoneNumber != null && model.BusinessPhoneNumber.Length >= 13)
                        model.BusinessPhoneNumber = rgx.Replace(model.BusinessPhoneNumber, "");
                    if (!string.IsNullOrEmpty(statename) && statename != "Temp")
                    {
                        var stateslist = context.CountryRegionNames.Where(p => p.REGION_NAME.ToUpper() == statename.ToUpper() && p.COUNTRY_CODE == model.CountryCode).Select(s => s.REGION_CODE).FirstOrDefault();
                        model.State = stateslist;
                    }

                    consultant.Name = model.Name;
                    consultant.PrimaryContactFirstName = model.PrimaryContactFirstName;
                    consultant.PrimaryContactLastName = model.PrimaryContactLastName;
                    consultant.SecondaryContactFirstName = model.SecondaryContactFirstName;
                    consultant.SecondaryContactLastName = model.SecondaryContactLastName;
                    consultant.PhysicalAddress = model.PhysicalAddress;
                    consultant.City = model.City;
                    consultant.State = model.State;
                    consultant.CountryCode = model.CountryCode;
                    consultant.PostalCode = model.PostalCode;
                    consultant.EmailAddress = model.EmailAddress;
                    consultant.HomeOfficeOverheadRateMax = model.HomeOfficeOverheadRateMax;
                    consultant.FCCM = model.FCCM;
                    consultant.FieldServiceOverheadRateMax = model.FieldServiceOverheadRateMax;
                    consultant.Multiplier = model.Multiplier;
                    consultant.ContactPhoneNumber = model.ContactPhoneNumber;
                    consultant.BusinessPhoneNumber = model.BusinessPhoneNumber;

                    consultant.LastUpdateDate = DateTime.Now;
                    consultant.LastUpdateUser = AHTD.Text.CommonText.StripNTDomain(CurrentUser.WindowsAccountName);

                    context.SaveChanges();

                }

                return Json(new { status = "OK", id = model.ConsultantId });
            }
            catch (DbEntityValidationException ex)
            {
                return Json(new { status = "ERROR", error = ex.GetExceptionMessages() });
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", error = ex.GetExceptionMessages() });
            }
        }

        //TODO fix according to invoice updates eventually probably
        public IEnumerable<Consultant> Filter(string term, int count = 20)
        {
            using (var context = new ConsultantContractsEntities())
            {
                IQueryable<Consultant> q = context.Consultants
                                                  .Include(c => c.Contracts)
                                                  .Include(c => c.Contracts.Select(con => con.Invoices));
                if (String.IsNullOrWhiteSpace(term))
                {
                    q = q.Where(c => c.Inactive == false);
                }
                else
                {
                    q = q.Where(c => c.TaxId.Contains(term) && c.Inactive == false)
                        .Union(
                            context.Consultants.Include(c => c.Contracts)
                                .Where(c => c.Name.Contains(term) && c.Inactive == false)
                        );
                }

                var conInv = q.Select(c => new
                {
                    consultant = c,
                    maxInv = (c.Contracts
                        .SelectMany(contract => contract.Invoices,
                            (contract, inv) => inv.InvoiceDate)
                        ).Max()
                });

                q = conInv.OrderByDescending(c => c.maxInv).Select(ci => ci.consultant).Take(count);

                return q.ToList();
            }
        }

        [HttpGet]
        public ActionResult GetStates()
        {
            using (var context = new ConsultantContractsEntities())
            {
                var stateslist = context.USStates.Where(p => p.USA_FLAG.ToUpper() == "Y").Select(s => new { Statename = s.STATE_NAME, State = s.STATE_CODE }).ToList();

                return Json(stateslist, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCountryRegionNames()
        {
            using (var context = new ConsultantContractsEntities())
            {
                var stateslist = context.CountryRegionNames.Where(p => p.COUNTRY_CODE.Equals("US")).ToList()
                                            .Select(j => new { countries = string.Format("{0}-{1}", j.COUNTRY_NAME.Trim(), j.REGION_NAME.Trim()), Countrycode = string.Format("{0}-{1}", j.COUNTRY_CODE.Trim(), j.REGION_CODE.Trim()) }).ToList();

                return Json(stateslist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

