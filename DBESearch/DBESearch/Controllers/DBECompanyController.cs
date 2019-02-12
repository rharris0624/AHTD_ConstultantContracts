using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBESearch.Models;
//using AHTD.Linq;
using DBESearch.ViewModels;

using System.Data;
using System.Data.Entity;

namespace DBESearch.Controllers
{
    public class DBECompanyController : Controller
    {
        private Exec_DBE_DirectoryEntities1 db = new Exec_DBE_DirectoryEntities1();
        // GET: DBECompany
        public ActionResult Index()
        {
            var zipcodes = db.DBECompanies.OrderBy(z => z.Zip).Select(z => z.Zip).Distinct();
            ViewBag.ZipCodeList = zipcodes.ToList();

            var states = db.DBECompanies.OrderBy(s => s.State).Select(s => s.State).Distinct();
            ViewBag.StateList = states.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(CompanySearchViewModel data)
        {
            if(data != null)
            {

            }
             return View("SearchResults");
        }

        public JsonResult companies(String term)
        {

            var t = db.DBECompanies.Where(c => c.CompanyName.Contains(term) || c.DBAName.Contains(term))
                                    .Select(c => new {c.CompanyId, c.CompanyName});
            return Json(t.Select(i => new { label = i.CompanyId + " - " + i.CompanyName, data = i }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult lastName(String searchterm)
        {
            object[] lastNames = db.DBECompanies.Where(c => c.OwnersLastName.ToUpper().Contains(searchterm.ToUpper())).Select(c => c.OwnersLastName).Distinct().ToArray();
            return Json(lastNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult firstName(String term)
        {
            object[] firstNames = db.DBECompanies.Where(c => c.OwnersFirstName.Contains(term)).Select(c => c.OwnersFirstName).Distinct().ToArray();
            return Json(firstNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partialform()
        {

            var results = db.NAICSCodes.ToList();
            return PartialView("~/Views/DBECompany/_NAICS.cshtml", results);
        }

        public ActionResult ItemPartialform()
        {
            var results = db.ItemCodes.ToList();
            return PartialView("~/Views/DBECompany/_ItemCodes.cshtml", results);
        }

        //need to send an array rather than individual items...ask Dick about this
        //public ActionResult SearchResults(Boolean DBE, Boolean ACDBE, Boolean SBE, Boolean MBE, String companylist, String BusinessDescription, String NAICS, String Item, String OwnersFirstName, String OwnersLastName, String City, String State, String Zip, String AreaCode, String County)
        public ActionResult SearchResults(CompanySearchViewModel searchData)
        {
            IQueryable<DBECompany> searchquery = db.DBECompanies;

            if (searchData != null)
            {
                searchquery = searchquery.Where(s => s.DBE.Equals(searchData.DBE ? true : false));
                searchquery = searchquery.Where(s => s.ACDBE.Equals(searchData.ACDBE ? true : false));
                searchquery = searchquery.Where(s => s.MBE.Equals(searchData.M ? true : false));
                searchquery = searchquery.Where(s => s.SBP.Equals(searchData.M ? true : false));
                if (searchData.CompanyId > 0)
                {
                    searchquery = searchquery.Where(c => c.CompanyId.Equals(searchData.CompanyId)).Select(c => c);
                }
                //if (!(BusinessDescription == null || BusinessDescription == ""))
                //{
                //searchquery = searchquery.Where(c => c. 
                //}

                //if (!(String.IsNullOrEmpty(NAICS)))
                //{
                //var testNAICS = new[]{"234990","484110"};
                //searchquery = searchquery.Include(z => z.CompanyItemCodes);
                //searchquery = CompanyNAICSCodes.Where(c => c.NAICSCode == "237310").Select(p => new {p.DBECompany.CompanyName}).ToArray();
                //}

                if (!(String.IsNullOrEmpty(searchData.OwnerFirstName)))
                {
                    searchquery = searchquery.Where(c => c.OwnersFirstName.Contains(searchData.OwnerFirstName)).Select(c => c).Distinct();
                }
                if (!(String.IsNullOrEmpty(searchData.OwnerLastName)))
                {
                    searchquery = searchquery.Where(c => c.OwnersLastName.Contains(searchData.OwnerLastName)).Select(c => c).Distinct();
                }
                if (searchData.Cities != null && searchData.Cities.Count() > 0)
                {
                    searchquery = searchquery.Join(searchData.Cities, a => a.City, b => b, (a, b) => new { a }).Select( c => c.a );
                }
                if (searchData.States != null && searchData.States.Count() > 0)
                {
                    searchquery = searchquery.Join(searchData.States, a => a.State, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (searchData.OwnerZipCodes != null && searchData.OwnerZipCodes.Count() > 0)
                {
                    searchquery = searchquery.Join(searchData.OwnerZipCodes, a => a.Zip, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (!string.IsNullOrEmpty(searchData.AreaCode))
                {
                    searchquery = searchquery.Where(c => c.Phone.StartsWith(searchData.AreaCode)); //Area Code is not split out in DB Need to string length check first????
                }
                //if(!(string.IsNullOrEmpty(County))) //currently, county does not exist in the database
                //{
                //
                //}

                //done with the querybuilding
                var results = searchquery.ToList();
                //var results = db.DBECompany.Where(c => c.CompanyName.ToUpper().Contains(companylist.ToUpper())).Select(c => c).ToArray();         
                return PartialView("~/Views/DBECompany/_SearchResults.cshtml", results);
            }
            return View();
        }

        public ActionResult Contact()
        {
            return PartialView("~/Views/DBECompany/_SearchResults.cshtml");
        }

        public JsonResult cities(String term)
        {
            //object[] cities = db.DBECompanies.Where(c => c.City.ToUpper().Contains(term.ToUpper())).Select(c => c.City).Distinct().ToArray();
            var t = db.DBECompanies.Where(c => c.City.Contains(term)).Select(c => new { c.City }).Distinct();
            return Json(t.Select(i => new { label = i.City, data = i.City }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayAll()
        {
            ViewBag.Message = "List all DBE Companies here.";
            return View();
        }

        /// 
        /// This is the view that is used to show the company contact information, NAICS codes, and departmental codes for a given company.
        /// The company id comes from the row click of the search results on Search page.
        /// 
        public ActionResult companyDetails(int? id)
        {
            List<CompanyViewModel> companyVM = new List<CompanyViewModel>();

            CompanyViewModel company = new CompanyViewModel();

            var companydatalist = db.DBECompanies.Where(c => c.CompanyId == id).Select(d => new
            {
                CompanyName = d.CompanyName,
                DBAName = d.DBAName,
                OwnersFirstName = d.OwnersFirstName,
                OwnersLastName = d.OwnersLastName,
                CompanyAddress = d.CompanyAddress,
                City = d.City,
                State = d.State,
                zip = d.Zip,
                Email = d.Email,
                Fax = d.Fax,
                AltFax = d.AltFax,
                District = d.District,
                DBE = d.DBE,
                ACDBE = d.ACDBE,
                SBP = d.SBP,
                MBE = d.MBE,
                Certified = d.Certified,
                CertificationDate = d.CertificationDate,
                DecertificationDate = d.DecertificationDate
            }).ToList();

            foreach (var item in companydatalist)
            {
                company.CompanyName = item.CompanyName;
                company.DBAName = item.DBAName;
                company.OwnersFirstName = item.OwnersFirstName;
                company.OwnersLastName = item.OwnersLastName;
                company.CompanyAddress = item.CompanyAddress + ", " + item.City + ", " + item.State;
                company.DBE = item.DBE;
                company.ACDBE = item.ACDBE;
                company.SBP = item.SBP;
                company.MBE = item.MBE;
                company.Certified = item.Certified;
                company.CertificationDate = item.CertificationDate;
                company.DecertificationDate = item.DecertificationDate;
                companyVM.Add(company);
            }

            //build me a list of the Departmental item codes for a given company and put the list in the object
            var codes = db.CompanyItemCodes.Where(c => c.CompanyID == id).Include("ItemCodes").ToList();

            List<CompanyItemCodeDesc> codelist = new List<CompanyItemCodeDesc>();

            foreach (var item in codes)
            {
                CompanyItemCodeDesc companycode = new CompanyItemCodeDesc();
                companycode.ItemCode = item.ItemCode;
                companycode.Description = item.ItemCode1.Description;
                companycode.Comments = item.Comments;
                codelist.Add(companycode);
            }

            company.CompanyItemCodesList = codelist;

            //build me a list of the NAICS codes for a given company and put the list in the object
            List<CompanyNAICSCodeDesc> NAICSList = new List<CompanyNAICSCodeDesc>();

            var NAICSCodes = db.DBECompanies.Where(c => c.CompanyId == id).Select(p => new { NAICSCode = p.NAICSCodes }).ToList();

            var naicsquery = (from comp in db.DBECompanies
                              from naics in comp.NAICSCodes.DefaultIfEmpty()
                              where comp.CompanyId == id
                              select new { naics.DBECompanies, naics.NAICSCode1, naics.Description }).ToList();

            foreach (var x in naicsquery)
            {
                CompanyNAICSCodeDesc naicsitem = new CompanyNAICSCodeDesc();
                naicsitem.NAICSCode = x.NAICSCode1;
                naicsitem.Description = x.Description;
                NAICSList.Add(naicsitem);
            }

            company.CompanyNAICSCodesList = NAICSList;

            return View(companyVM);
        }

    }
}

