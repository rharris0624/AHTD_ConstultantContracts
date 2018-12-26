using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.Controllers
{
    public class VendorLookupController : SecuredController
    {
        //
        // GET: /VendorLookup/

        public ActionResult VendorSearch(string term)
        {
            using (var context = new FiscalSer_VendorMasterEntities())
            {

                var vendors = context.VENDOR_MASTER
                    .Where(v =>
                        v.FED_ID.StartsWith(term) ||
                        v.BUS_NAME.Contains(term) ||
                        v.VENDOR_NAME.Contains(term))
                    .OrderByDescending(v => v.LAST_DATE)
                    .Take(12)
                    .ToList()
                    .Select(v => new
                    {
                        value = new { TaxId = v.FED_ID, SeqNo = v.SEQ_NO },
                        label = String.Format("{0}-{1}: {2}", v.FED_ID, v.SEQ_NO, v.VENDOR_NAME)
                    }).ToList();

                return Json(vendors, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult VendorInfo(string taxId, int seqNo)
        {
            using (var context = new FiscalSer_VendorMasterEntities())
            {
                var consultant = new ConsultantContractsEntities().Consultants.FirstOrDefault(p => p.TaxId == taxId && p.SeqNo == seqNo);
                var vendor = context.VENDOR_MASTER.SingleOrDefault(v => v.FED_ID == taxId && v.SEQ_NO == seqNo);

                if (vendor == null)
                {
                    if (consultant != null)
                    {
                        return FillConsultant(consultant);
                    }
                    else { return Json(new { Type = "Conusltant" }, JsonRequestBehavior.AllowGet); }
                }

                return Json(new
                {
                    Type = "Vendor",
                    TaxId = vendor.FED_ID.Trim(),
                    SeqNo = vendor.SEQ_NO,
                    Name = vendor.VENDOR_NAME,
                    Address = String.Format("{0}, {1}", vendor.ADDRESS_1.Trim(), vendor.ADDRESS_2.Trim()),
                    City = vendor.CITY.Trim(),
                    State = vendor.STATE.Trim(),
                    CountryCode = vendor.COUNTRY_CODE.Trim(),
                    PostalCode = vendor.ZIP.Trim(),
                    VendorAddress = vendor.ADDRESS_1.Trim(),
                    Vendorfulladdress = vendor.CITY.Trim() + ", " + vendor.STATE.Trim() + ", " + vendor.COUNTRY_CODE.Trim() + ", " + vendor.ZIP.Trim(),
                    Phone = vendor.PHONE_NO.Trim(),
                    Fax = vendor.FAX_NO.Trim(),

                    PrimaryContactFirstName = consultant != null ? consultant.PrimaryContactFirstName : "",
                    PrimaryContactLastName = consultant != null ? consultant.PrimaryContactLastName : "",
                    EmailAddress = consultant != null ? consultant.EmailAddress : "",
                    SecondaryContactFirstName = consultant != null ? consultant.SecondaryContactFirstName : "",
                    SecondaryContactLastName = consultant != null ? consultant.SecondaryContactLastName : "",
                    PhysicalAddress = consultant != null ? consultant.PhysicalAddress : "",
                    HomeOfficeOverheadRateMax = consultant != null && consultant.HomeOfficeOverheadRateMax.HasValue ? consultant.HomeOfficeOverheadRateMax.Value:0,
                    FCCM = consultant != null && consultant.FCCM.HasValue ? consultant.FCCM.Value : 0,
                    FieldServiceOverheadRateMax = consultant != null && consultant.FieldServiceOverheadRateMax.HasValue ? consultant.FieldServiceOverheadRateMax.Value : 0,
                    Multiplier = consultant != null && consultant.Multiplier.HasValue ? consultant.Multiplier.Value : 0,
                    //BusinessPhone = consultant != null && consultant.BusinessPhoneNumber != null ? consultant.BusinessPhoneNumber : "",
                    //ContactPhone = consultant != null && consultant.ContactPhoneNumber != null ? consultant.ContactPhoneNumber : "",

                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FillConsultant(Consultant consultant)
        {
            string errormessage = "Tax Id and Seq No not on Vendor Master";
            return Json(new
            {
                Type = "Conusltant",
                TaxId = consultant.TaxId.Trim(),
                SeqNo = consultant.SeqNo,
                Name = consultant.Name,
                PrimaryContactFirstName = consultant.PrimaryContactFirstName,
                PrimaryContactLastName = consultant.PrimaryContactLastName,
                EmailAddress = consultant.EmailAddress,
                SecondaryContactFirstName = consultant.SecondaryContactFirstName,
                SecondaryContactLastName = consultant.SecondaryContactLastName,
                PhysicalAddress = consultant.PhysicalAddress,
                City = consultant.City,
                State = consultant.State,
                CountryCode = consultant.CountryCode,
                PostalCode = consultant.PostalCode,
                HomeOfficeOverheadRateMax = consultant.HomeOfficeOverheadRateMax,
                FCCM = consultant.FCCM,
                FieldServiceOverheadRateMax = consultant.FieldServiceOverheadRateMax,
                Multiplier = consultant.Multiplier,
                //ConsultantPhone = consultant.ContactPhoneNumber,
                //BusinessPhone = consultant.BusinessPhoneNumber,

                Msg = errormessage
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
