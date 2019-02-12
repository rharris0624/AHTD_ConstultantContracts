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
    public class ItemCodesController : Controller
    {
        private Exec_DBE_DirectoryEntities1 db = new Exec_DBE_DirectoryEntities1();
        // GET: ItemCodes
        public ActionResult Index()
        {
            return View(db.ItemCodes.ToList());
        }
        [HttpPost]
        public JsonResult List(int jtStartIndex = 0, int JtPageSize = 0, string jtSorting = null)
        {
            try
            {
                //object[] ItemCodeList = db.ItemCodes.Select(c => new { ItemCode = c.ItemCode, Description = c.Description }).ToArray();
                var ItemCodeList = db.ItemCodes.Select(c => new { ItemCode = c.ItemCode1, Description = c.Description }).OrderBy(c => c.ItemCode).Skip(jtStartIndex).Take(JtPageSize).ToList();
                int recordCount = db.NAICSCodes.Count();

                return Json(new { Result = "OK", Records = ItemCodeList, TotalRecordCount = recordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}