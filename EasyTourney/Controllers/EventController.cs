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
    public class EventController : Controller
    {
        private DBEasyTourneyEntities db = new DBEasyTourneyEntities();

        // GET: Event
        public ActionResult Index()
        {
            return View(db.tblEvent.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvent.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GUID,Name,Description,StarDate,CreatedDate,Price,City,Country,MaxParticipants,IsActive")] tblEvent tblEvent)
        {
            if (ModelState.IsValid)
            {
                tblEvent.GUID = Guid.NewGuid();
                db.tblEvent.Add(tblEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEvent);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvent.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GUID,Name,Description,StarDate,CreatedDate,Price,City,Country,MaxParticipants,IsActive")] tblEvent tblEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEvent);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvent.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tblEvent tblEvent = db.tblEvent.Find(id);
            db.tblEvent.Remove(tblEvent);
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
    }
}
