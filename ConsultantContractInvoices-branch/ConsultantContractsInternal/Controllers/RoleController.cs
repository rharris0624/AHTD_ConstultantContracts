using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultantContractsInternal.Models;

namespace ConsultantContractsInternal.Controllers
{   
    public class RoleController : Controller
    {
        private ArDOT_UserProvEntities1 context = new ArDOT_UserProvEntities1();

        //
        // GET: /Role/

        public ViewResult Index()
        {
            return View(context.Roles.ToList());
        }

        //
        // GET: /Role/Details/5

        public ViewResult Details(string id)
        {
            Role role = context.Roles.Single(x => x.RoleId == id);
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            ViewBag.PossibleApplications = context.Applications;
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.PossibleApplications = context.Applications;
            return View(role);
        }
        
        //
        // GET: /Role/Edit/5
 
        public ActionResult Edit(string id)
        {
            Role role = context.Roles.Single(x => x.RoleId == id);
            ViewBag.PossibleApplications = context.Applications;
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleApplications = context.Applications;
            return View(role);
        }

        //
        // GET: /Role/Delete/5
 
        public ActionResult Delete(string id)
        {
            Role role = context.Roles.Single(x => x.RoleId == id);
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = context.Roles.Single(x => x.RoleId == id);
            context.Roles.Remove(role);
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