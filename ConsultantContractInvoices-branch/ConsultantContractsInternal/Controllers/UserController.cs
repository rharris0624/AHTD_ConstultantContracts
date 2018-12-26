using ConsultantContractsInternal.Models;
//using ConsultantContractsInternal.Security;
using ConsultantContractsInternal.Security.Attributes;
using ConsultantContractsInternal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultantContractsInternal.Controllers
{
    //[AHTDAuthorize(Roles = "Admin,Developer")]
    public class UserController : Controller
    {
        private ArDOT_UserProvEntities1 context = new ArDOT_UserProvEntities1();

        //
        // GET: /User/

        public ViewResult Index()
        {
            IList<User> userList = context.Users.Include(p => p.ApplicationSecurities).ToList();
            //return View(context.Users.ToList());
            return View(userList);
        }

        //
        // GET: /User/

        public JsonResult GetUserList(string term)
        {
            var t = context.Users.Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term) || p.UserId.Contains(term)).Select( m => new {m.FirstName, m.LastName, m.UserId});
            var g = Json(t.Select(i => new { label = i.UserId + " - " + i.LastName + ", " + i.FirstName, data = i }), JsonRequestBehavior.AllowGet);
            return g;
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(string id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(string id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = context.Users.Single(x => x.UserId == id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}