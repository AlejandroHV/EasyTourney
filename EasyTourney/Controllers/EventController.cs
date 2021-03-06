﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyTourney.Models;
using EasyTourney.Bll;
using EasyTourney.ViewModels;
using EasyTourney.Filters;
using EasyTourney.Common;

namespace EasyTourney.Controllers
{
    public class EventController : Controller
    {
        private DBEasyTourneyEntities db = new DBEasyTourneyEntities();
        private EventBll eventBll = new EventBll();

        // GET: Event
        [IsAdminFilter]
        public ActionResult Index()
        {
            tblUser user = null;

            if (Session["USER"] != null)
            {
                user = (EasyTourney.Models.tblUser)Session["USER"];
            }

            if (user != null)
            {
                Guid managerGuid = user.GUID;
                return View(eventBll.getEventsByManager(managerGuid));
            }

            return RedirectToAction("Index","Home");
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
            tblUser user = null;

            if (Session["USER"] != null)
            {
                user = (EasyTourney.Models.tblUser)Session["USER"];
            }

            if (ModelState.IsValid && user != null)
            {
                Guid eventGuid = Guid.NewGuid();
                tblEvent.GUID = eventGuid;
                db.tblEvent.Add(tblEvent);

                tblUserEvents userEvent = new tblUserEvents();
                userEvent.GUID = Guid.NewGuid();
                userEvent.EventId = eventGuid;
                userEvent.UserId = user.GUID;
                db.tblUserEvents.Add(userEvent);

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

        [IsParticipantFilter]
        public ActionResult RegisterForEvent(Guid? eventGuid)
        {
            tblUser user = (tblUser)Session["USER"];

            RegisterForEvent registerForEvent = new RegisterForEvent
            {                
                selectedEvent = db.tblEvent.Find(eventGuid)
                ,
                userGuid = user.GUID
            };

            return View(registerForEvent);
        }

        [HttpPost, ActionName("RegisterForEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterForEventConfirmed(Guid eventGuid)
        {
            if(ModelState.IsValid)
            {
                tblUser user = (tblUser)Session["USER"];
                tblUserEvents userEvent = new tblUserEvents();
                userEvent.UserId = user.GUID;
                userEvent.EventId = eventGuid;
                userEvent.GUID = Guid.NewGuid();
                db.tblUserEvents.Add(userEvent);

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Home(Guid id)
        {
            EventIndex eventInfo = new EventIndex();
            eventInfo.eventInfo = db.tblEvent.Find(id);
            eventInfo.eventParticipants = db.tblUserEvents.Where(e => e.tblEvent.GUID == id && !e.tblUser.tblRol.Name.Equals("Manager")).ToList();
            return View(eventInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult eventMessages(Guid eventGuid)
        {
            ViewBag.EventGuid = eventGuid;
            return View("_EventMessages", eventBll.getMessagesByEvent(eventGuid));
        }

        //[HttpPost]
        //public ActionResult eventMessages(Guid eventGuid)
        //{
        //    ViewBag.EventGuid = eventGuid;
        //    return View("_EventMessages", eventBll.getMessagesByEvent(eventGuid));
        //}

        public ActionResult createComment(Guid? eventGuid)
        {
            ViewBag.EventGuid = eventGuid;
            return View("_EventCommentForm", new SaveEventMessageModel());
        }

        [HttpPost]
        public ActionResult saveEventMessages(SaveEventMessageModel eventMessageModel)
        {
            if (ModelState.IsValid)
            {
                tblUser user = (tblUser)Session["USER"];

                tblMessage message = new tblMessage();
                Guid messageId = Guid.NewGuid();
                message.Id = messageId;
                message.Content = eventMessageModel.message.Content;
                message.CategoryId = (int)Enums.messageType.FindTeam;
                message.UserId = user.GUID;
                db.tblMessage.Add(message);

                tblEventMessages eventMessage = new tblEventMessages();
                eventMessage.EventId = eventMessageModel.relatedEvent.GUID;
                eventMessage.MessageId = messageId;
                db.tblEventMessages.Add(eventMessage);

                db.SaveChanges();
                return RedirectToAction("Home", "Event", new { id = eventMessage.EventId});
            }
            
            return View("Index", "Home");
        }
    }
}
