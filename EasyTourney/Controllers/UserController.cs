using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyTourney.Models;

namespace EasyTourney.Controllers
{
    public class UserController : Controller
    {
        private DBEasyTourneyEntities db = new DBEasyTourneyEntities();

        // GET: User
        public ActionResult Index()
        {
            var tblUser = db.tblUser.Include(t => t.tblRol);
            return View(tblUser.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.tblRol, "Id", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GUID,FirstName,LastName,Email,Phone,City,Country,RolId,Pass")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                tblUser.GUID = Guid.NewGuid();
                db.tblUser.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.tblRol, "Id", "Name", tblUser.RolId);
            return View(tblUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.tblRol, "Id", "Name", tblUser.RolId);
            return View(tblUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GUID,FirstName,LastName,Email,Phone,City,Country,RolId,Pass")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.tblRol, "Id", "Name", tblUser.RolId);
            return View(tblUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tblUser tblUser = db.tblUser.Find(id);
            db.tblUser.Remove(tblUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {

            return View();
        }

    }
}
