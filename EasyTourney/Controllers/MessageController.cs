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
    public class MessageController : Controller
    {
        private DBEasyTourneyEntities db = new DBEasyTourneyEntities();

        // GET: Message
        public ActionResult Index()
        {
            var tblMessage = db.tblMessage.Include(t => t.tblCategory);
            return View(tblMessage.ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMessage tblMessage = db.tblMessage.Find(id);
            if (tblMessage == null)
            {
                return HttpNotFound();
            }
            return View(tblMessage);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.tblCategory, "Id", "Name");
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Content,UserId,IsActive")] tblMessage tblMessage)
        {
            if (ModelState.IsValid)
            {
                tblMessage.Id = Guid.NewGuid();
                db.tblMessage.Add(tblMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.tblCategory, "Id", "Name", tblMessage.CategoryId);
            return View(tblMessage);
        }

        // GET: Message/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMessage tblMessage = db.tblMessage.Find(id);
            if (tblMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.tblCategory, "Id", "Name", tblMessage.CategoryId);
            return View(tblMessage);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Content,UserId,IsActive")] tblMessage tblMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.tblCategory, "Id", "Name", tblMessage.CategoryId);
            return View(tblMessage);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMessage tblMessage = db.tblMessage.Find(id);
            if (tblMessage == null)
            {
                return HttpNotFound();
            }
            return View(tblMessage);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            tblMessage tblMessage = db.tblMessage.Find(id);
            db.tblMessage.Remove(tblMessage);
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
