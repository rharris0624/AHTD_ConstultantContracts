using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;
using Newtonsoft.Json;
using ConsultantContractsInternal.ViewModels;
using System.Data;
using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.Security.Attributes;
using System.Globalization;
using Elmah;

namespace ConsultantContractsInternal.Controllers
{
    public class SuppAgreementController : SecuredController
    {

        //takes in contract code
        public ActionResult SuppAgreementsByContract(int id)
        {
            ViewBag.ContractCode = id;

            using (var context = new ConsultantContractsEntities())
            {
                var r = context.SuppAgreements
                    .Include(sa => sa.SuppSalaryRates)
                    .Include(sa => sa.SuppServiceRates)
                    .Include(sa => sa.SuppAllotments)
                    .Include(sa => sa.SuppSubConsultantInfoes)
                    .Include(sa => sa.SuppSubConsultantInfoes.Select(ssc => ssc.SubConsultant))
                    .Include(sa => sa.SuppSubConsultantInfoes.Select(ssc => ssc.SuppSubConsultantSalaryRates))
                    .Include(sa => sa.SuppSubConsultantInfoes.Select(ssc => ssc.SuppSubConsultantServiceRates))
                    .Where(sa => sa.ContractCode == id).ToList();

                if (Roles.Admin.Split(',').Contains(CurrentUser.Role))
                    ViewBag.WriteAccess = true;
                else
                    ViewBag.WriteAccess = false;

                return PartialView(r);
            }

            
        }


        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult SaveRemark(int contractCode, string suppAgreement, string text)
        {
            using (var context = new ConsultantContractsEntities())
            {
                var supp = context.SuppAgreements.SingleOrDefault(s => s.ContractCode == contractCode && s.SuppNo == suppAgreement);

                if (supp == null) return new HttpNotFoundResult();

                supp.Remarks = text;
                supp.RemarkLastEditDate = DateTime.Now;
                supp.RemarkLastEditUser = CurrentUser.UserName;

                context.SaveChanges();

                return Json(new { msg = "OK" });
            }
        }

        //takes in contract code
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult AddSupplemental(int id)
        {

            ViewBag.contractId = id;

            using (var context = new ConsultantContractsEntities())
            {
                var cd = context.Contracts.Include(c => c.Consultant)
                                          .Include(c => c.ContractType)
                                          .Include(c => c.WorkTypes)
                                          .Include(c => c.SalaryRates)
                                          .Include(c => c.ServiceRates)
                                          .Include(c => c.ContractAllotments)
                                          .Include(c => c.SuppAgreements)
                                          .Include(c => c.ContractSubConsultants)
                                          .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultantSalaryRates))
                                          .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultantServiceRates))
                                          .Include(c => c.ContractSubConsultants.Select(csc => csc.SubConsultant))
                                          .SingleOrDefault(c => c.ContractCode == id);
                if (cd == null)
                {
                    //error here better
                    throw new NotImplementedException();
                }
                if (cd.ScheduledCompletionDate != null)
                {
                    DateTime estComplDt = (DateTime)cd.ScheduledCompletionDate;
                    ViewBag.EstimatedCompletionDate = estComplDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    ViewBag.EstimateCompletionDate = DateTime.Now.ToShortDateString();
                }
                var lastSupp = cd.SuppAgreements.OrderByDescending(s => s.LastUpdateDate).FirstOrDefault();
                var jsonConfig = new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                ViewBag.contractData = JsonConvert.SerializeObject(new
                {
                    Contract = cd,
                    latestSuppNo = lastSupp == null ? null : lastSupp.SuppNo
                },jsonConfig);

            }

            return View();
        }

        [HttpPost]
        [AHTDAuthorize(Roles = Roles.ContractEntryRoles)]
        public ActionResult AddSupplemental(NewSuppVM model)
        {
            try
            {
                using (var context = new ConsultantContractsEntities())
                {
                    //get contract to edit
                    var contract = context.Contracts
                        .Include(c => c.SalaryRates)
                        .Include(c => c.ServiceRates)
                        .Single(c => c.ContractCode == model.ContractCode);

                    //generate supp agreement
                    var newSuppAgreement = model.PopulateSuppAgreement(context.SuppAgreements.Create());

                    // Edit orignal contract values
                    contract.ContractCeiling = model.ContractCeiling;
                    contract.T1SvcsCeiling = model.T1SvcsCeiling;
                    contract.T1FixedFeeMax = model.T1FixedFeeMax;
                    contract.HomeOfficeOverheadRateMax = model.HomeOfficeOverheadRateMax;
                    contract.T2SvcsCeiling = model.T2SvcsCeiling;
                    contract.T2FixedFeeMax = model.T2FixedFeeMax;
                    contract.FieldServiceOverheadRateMax = model.FieldServiceOverheadRateMax;
                    contract.Multiplier = model.Multiplier;

                    //suppagreement audit
                    newSuppAgreement.LastUpdateDate = DateTime.Now;
                    newSuppAgreement.LastUpdateUser = CurrentUser.UserName;
                    contract.LastUpdateDate = DateTime.Now;
                    contract.LastUpdateUser = CurrentUser.UserName;
                    contract.CompletionDate = model.CompletionDate != null ? model.CompletionDate : DateTime.MinValue;

                    //check for subcon on vm
                    #region subconsultants
                    if (model.SubConsultants != null)
                    {
                        //for each subcon
                        model.SubConsultants.ForEach(subcon =>
                        {
                            //get subcon entity
                            var sc = subcon.ToSuppSubConsultant();
                            sc.LastUpdateDate = DateTime.Now;
                            sc.LastUpdateUser = CurrentUser.UserName;

                            //try and get subcon on contract
                            var sccheck = context.ContractSubConsultants
                                .Include(c => c.SubConsultantSalaryRates)
                                .Include(c => c.SubConsultantServiceRates)
                                .SingleOrDefault(csc => csc.ContractCode == contract.ContractCode && csc.SubConsultantId == sc.SubConsultantId);

                            //if subcon is new
                            if (sccheck == null)
                            {
                                //make entity
                                sccheck = subcon.ToContractSubConsultant();
                                //add to contract
                                contract.ContractSubConsultants.Add(sccheck);
                            }
                            //else update the existing record
                            else
                            {

                                if (subcon.ContractCeilingChanged)
                                {
                                    sccheck.ContractCeiling = subcon.ContractCeiling;
                                }
                                if (subcon.T1ServicesChanged)
                                {
                                    sccheck.T1SvcsCeiling = subcon.T1Services;
                                }
                                if (subcon.T1FixedFeeChanged)
                                {
                                    sccheck.T1FixedFeeMax = subcon.T1FixedFee;
                                }
                                if (subcon.HomeOfficeOverheadChanged)
                                {
                                    sccheck.HomeOfficeOverheadRateMax = subcon.HomeOfficeOverhead;
                                }
                                if (subcon.FCCMChanged)
                                {
                                    sccheck.FCCM = subcon.FCCM;
                                }
                                if (subcon.T2ServicesChanged)
                                {
                                    sccheck.T2SvcsCeiling = subcon.T2Services;
                                }
                                if (subcon.T2FixedFeeChanged)
                                {
                                    sccheck.T2FixedFeeMax = subcon.T2FixedFee;
                                }
                                if (subcon.FieldServiceOverheadChanged)
                                {
                                    sccheck.FieldServiceOverheadRateMax = subcon.FieldServiceOverhead;
                                }
                                if (subcon.MultiplierChanged)
                                {
                                    sccheck.Multiplier = subcon.Multiplier;
                                }
                            }
                            sccheck.LastUpdateDate = DateTime.Now;
                            sccheck.LastUpdateUser = CurrentUser.UserName;

                            //check for salary rates on current subcon
                            if (subcon.SalaryRates != null)
                            {
                                subcon.SalaryRates.ForEach(sr =>
                                {
                                    if (sr.RateMaxChanged || sr.RateMinChanged || sr.PendingDelete || !sr.existingRecord)
                                    {
                                        //get subcon salary rate entity
                                        var s = sr.ToSubConSalaryRate();
                                        s.SubConsultantId = subcon.ConsultantId;
                                        s.LastUpdateDate = DateTime.Now;
                                        s.LastUpdateUser = CurrentUser.UserName;

                                        //add to supp subcon entity
                                        sc.SuppSubConsultantSalaryRates.Add(s);

                                        //check existing
                                        if (sr.existingRecord) // Delete/Edit Salary Rate
                                        {
                                            //if it exists get it from contract sub con
                                            var csr = sccheck.SubConsultantSalaryRates.Single(jt => jt.JobTitle == sr.JobTitle);
                                            if (sr.PendingDelete)
                                            {
                                                //delete
                                                context.SubConsultantSalaryRates.Remove(csr);
                                            }
                                            else
                                            {
                                                //edit
                                                csr.RateMax = sr.RateMax;
                                                csr.RateMin = sr.RateMin;
                                                csr.LastUpdateDate = DateTime.Now;
                                                csr.LastUpdateUser = CurrentUser.UserName;
                                            }
                                        }
                                        else // Create Salary Rate
                                        {
                                            //if not existing add
                                            var csr = context.SubConsultantSalaryRates.Create();
                                            csr.SubConsultantId = subcon.ConsultantId;
                                            csr.JobTitle = sr.JobTitle;
                                            csr.RateMax = sr.RateMax;
                                            csr.RateMaxOrig = sr.RateMax;
                                            csr.RateMin = sr.RateMin;
                                            csr.RateMinOrig = sr.RateMin;
                                            csr.LastUpdateDate = DateTime.Now;
                                            csr.LastUpdateUser = CurrentUser.UserName;
                                            sccheck.SubConsultantSalaryRates.Add(csr);
                                        }
                                    }
                                });

                            }
                            //check for services rates on sub con
                            if (subcon.ServiceRates != null)
                            {
                                subcon.ServiceRates.ForEach(sr =>
                                {
                                    if (sr.RateMaxChanged || sr.RateMinChanged || sr.PendingDelete || !sr.existingRecord)
                                    {
                                        //get sub con service rates entities
                                        var s = sr.ToSubConServiceRate();
                                        s.SubConsultantId = subcon.ConsultantId;
                                        s.LastUpdateDate = DateTime.Now;
                                        s.LastUpdateUser = CurrentUser.UserName;

                                        //add to supp subcon
                                        sc.SuppSubConsultantServiceRates.Add(s);

                                        if (sr.existingRecord)
                                        {
                                            //if exists get it from subcon entity to edit
                                            var csr = sccheck.SubConsultantServiceRates.Single(sn => sn.ServiceName == sr.ServiceName);
                                            if (sr.PendingDelete) // Delete
                                            {
                                                context.SubConsultantServiceRates.Remove(csr);
                                            }
                                            else
                                            {
                                                // Edit
                                                csr.RateMax = sr.RateMax;
                                                csr.RateMin = sr.RateMin;
                                                csr.LastUpdateDate = DateTime.Now;
                                                csr.LastUpdateUser = CurrentUser.UserName;
                                            }
                                        }
                                        else // Create
                                        {
                                            //get subcon service rate entity to add to contract subcon
                                            var csr = context.SubConsultantServiceRates.Create();
                                            csr.SubConsultantId = subcon.ConsultantId;
                                            csr.ServiceName = sr.ServiceName;
                                            csr.RateMax = sr.RateMax;
                                            csr.RateMaxOrig = sr.RateMax;
                                            csr.RateMin = sr.RateMin;
                                            csr.RateMinOrig = sr.RateMin;
                                            csr.LastUpdateDate = DateTime.Now;
                                            csr.LastUpdateUser = CurrentUser.UserName;
                                            sccheck.SubConsultantServiceRates.Add(csr);
                                        }
                                    }
                                });
                            }

                            newSuppAgreement.SuppSubConsultantInfoes.Add(sc);
                        });
                    }
                    #endregion
                    //Salary Rates
                    #region SalaryRates
                    if (model.SalaryRates != null)
                    {
                        model.SalaryRates.ForEach(sr =>
                        {
                            if (sr.RateMaxChanged || sr.RateMinChanged || sr.PendingDelete || !sr.existingRecord)
                            {
                                var s = sr.ToSalaryRate();
                                s.LastUpdateDate = DateTime.Now;
                                s.LastUpdateUser = CurrentUser.UserName;

                                newSuppAgreement.SuppSalaryRates.Add(s);

                                if (sr.existingRecord) // Delete/Edit Salary Rate
                                {

                                    var csr = contract.SalaryRates.SingleOrDefault(jt => jt.JobTitle == sr.JobTitle);
                                    if (sr.PendingDelete)
                                    {
                                        context.SalaryRates.Remove(csr);
                                    }
                                    else
                                    {
                                        // Edit
                                        csr.RateMax = sr.RateMax;
                                        csr.RateMin = sr.RateMin;
                                        csr.LastUpdateDate = DateTime.Now;
                                        csr.LastUpdateUser = CurrentUser.UserName;
                                    }
                                }
                                else // Create Salary Rate
                                {
                                    var csr = context.SalaryRates.Create();
                                    csr.JobTitle = sr.JobTitle;
                                    csr.RateMax = sr.RateMax;
                                    csr.RateMin = sr.RateMin;
                                    csr.LastUpdateDate = DateTime.Now;
                                    csr.LastUpdateUser = CurrentUser.UserName;
                                    contract.SalaryRates.Add(csr);
                                }
                            }
                        });
                    }
                    #endregion
                    //Service Rates
                    #region
                    if (model.ServiceRates != null)
                    {
                        model.ServiceRates.ForEach(sr =>
                        {
                            if (sr.RateMaxChanged || sr.RateMinChanged || sr.PendingDelete || !sr.existingRecord)
                            {
                                var s = sr.ToServiceRate();
                                s.LastUpdateDate = DateTime.Now;
                                s.LastUpdateUser = CurrentUser.UserName;

                                newSuppAgreement.SuppServiceRates.Add(s);

                                if (sr.existingRecord)
                                {

                                    var csr = contract.ServiceRates.Single(sn => sn.ServiceName == sr.ServiceName);
                                    if (sr.PendingDelete) // Delete
                                    {
                                        context.ServiceRates.Remove(csr);
                                    }
                                    else
                                    {
                                        // Edit
                                        csr.RateMax = sr.RateMax;
                                        csr.RateMin = sr.RateMin;
                                        csr.LastUpdateDate = DateTime.Now;
                                        csr.LastUpdateUser = CurrentUser.UserName;
                                    }
                                }
                                else // Create
                                {
                                    var csr = context.ServiceRates.Create();
                                    csr.ServiceName = sr.ServiceName;
                                    csr.RateMax = sr.RateMax;
                                    csr.RateMin = sr.RateMin;
                                    csr.LastUpdateDate = DateTime.Now;
                                    csr.LastUpdateUser = CurrentUser.UserName;
                                    contract.ServiceRates.Add(csr);
                                }
                            }
                        });
                    }
                    #endregion
                    //T1 Allotments
                    #region T1Allotments
                    if (model.T1Allotments != null)
                    {
                        model.T1Allotments.ForEach(avm =>
                        {
                            if (avm.AllottedAmountChanged)
                            {
                                var a = avm.ToAllotment();
                                a.LastUpdateDate = DateTime.Now;
                                a.LastUpdateUser = CurrentUser.UserName;

                                newSuppAgreement.SuppAllotments.Add(a);

                                var contractAllotment = context.ContractAllotments.SingleOrDefault(c => c.Func == avm.Func && c.FAP == avm.FAP && c.ContractCode == contract.ContractCode);
                                if (contractAllotment != null)
                                {
                                    contractAllotment.AllottedAmt = avm.AllottedAmount;
                                }
                            }
                        });
                    }
                    #endregion
                    //T2 Allotments
                    #region T2 Allotments
                    if (model.T2Allotments != null)
                    {
                        model.T2Allotments.ForEach(avm =>
                        {
                            if (avm.AllottedAmountChanged)
                            {
                                var a = avm.ToAllotment();
                                a.LastUpdateDate = DateTime.Now;
                                a.LastUpdateUser = CurrentUser.UserName;

                                newSuppAgreement.SuppAllotments.Add(a);


                                var contractAllotment = context.ContractAllotments.SingleOrDefault(c => c.Func == avm.Func && c.FAP == avm.FAP && c.ContractCode == contract.ContractCode);
                                if (contractAllotment != null)
                                { 
                                    contractAllotment.AllottedAmt = avm.AllottedAmount; 
                                }
                            }
                        });
                    }
                    #endregion

                    contract.SuppAgreements.Add(newSuppAgreement);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                        return Json(new { Status = "ERROR", msg = ex });
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                        return Json(new { Status = "ERROR", msg = ex });
                    }

                    return Json(new { Status = "OK", contractCode = contract.ContractCode }, JsonRequestBehavior.DenyGet);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Error(ex));
                return Json(new { Status = ex.Source, msg = ex.Message });
            }
        }

    }
}
