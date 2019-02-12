using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBESearch.Models;

namespace DBESearch.Controllers
{
    public class NAICSController : Controller
    {
        private Exec_DBE_DirectoryEntities1 db = new Exec_DBE_DirectoryEntities1();

        //
        // GET: /NAICS/
        [HttpPost]
        public JsonResult NAICSList(int jtStartIndex = 0, int JtPageSize = 0, string jtSorting = null)
        {
            try
            {
                //object[] NAICSList = db.NAICSCodes.Select(c => new { NAICSCode = c.NAICSCode, Description = c.Description }).ToArray(); 

                //List<NAICSCodes> NAICSList = new List<NAICSCodes>();
                var NAICSList = db.NAICSCodes.Select(c => new { NAICSCode = c.NAICSCode1, Description = c.Description }).OrderBy(c => c.NAICSCode).Skip(jtStartIndex).Take(JtPageSize).ToList();
                int recordCount = db.NAICSCodes.Count();

                return Json(new { Result = "OK", Records = NAICSList, TotalRecordCount = recordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        


        public ActionResult Index()
        {
            return View(db.NAICSCodes.ToList());
        }

        //
        // GET: /NAICS/Details/5

        public ActionResult Details(string id = null)
        {
            NAICSCode naicscodes = db.NAICSCodes.Find(id);
            if (naicscodes == null)
            {
                return HttpNotFound();
            }
            return View(naicscodes);
        }

     

      

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}