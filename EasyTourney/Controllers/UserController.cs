using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyTourney.Models;
using EasyTourney.Models.Entities;

namespace EasyTourney.Controllers
{
    public class UserController : Controller
    {
        private DBEasyTourneyEntities db = new DBEasyTourneyEntities();

        private DBEasyTourneyEntities DBContext
        {
            get
            {
                if (db == null)
                {
                    db = new DBEasyTourneyEntities();
                }
                return db;
            }
        }

        // GET: User
        public ActionResult Index()
        {
            var tblUser = DBContext.tblUser.Include(t => t.tblRol);
            return View(tblUser.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = DBContext.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(DBContext.tblRol, "Id", "Name");
            tblUser user = new tblUser();
            user.AllPreferences = db.tblPreference.ToList();
            user.SelectedPreferences = new List<tblPreference>();
            user.PostedReference = new PostedPreference();
            user.PostedReference.preferenceId = new string[0];
            return View(user);
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,Phone,City,Country,IsEventAdmin,Pass,Preferences,SelectedPreferences,PostedReference")]  tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                List<tblRol> rolList = DBContext.tblRol.ToList();
                tblUser.GUID = Guid.NewGuid();
                if (tblUser.IsEventAdmin)
                {
                    tblRol managerRol = rolList.Where(rol => rol.Name == "Manager").FirstOrDefault();
                    tblUser.RolId = managerRol.Id;
                }
                else
                {
                    tblRol participantRol = rolList.Where(rol => rol.Name == "Participant").FirstOrDefault();
                    tblUser.RolId = participantRol.Id;
                    foreach (var item in tblUser.PostedReference.preferenceId)
                    {
                        Guid preferenceGuid = Guid.NewGuid();
                        Guid.TryParse(item, out preferenceGuid);
                        tblUserPreference userPreference = new tblUserPreference();
                        userPreference.GUID = Guid.NewGuid();
                        userPreference.PreferenceId = preferenceGuid;
                        userPreference.UserId = tblUser.GUID;
                        DBContext.Entry<tblUserPreference>(userPreference).State = EntityState.Added;
                        DBContext.tblUserPreference.Add(userPreference);
                    }

                }
                DBContext.tblUser.Add(tblUser);
                DBContext.SaveChanges();

                return RedirectToAction("Login");
            }

            ViewBag.RolId = new SelectList(DBContext.tblRol, "Id", "Name", tblUser.RolId);


            return View(tblUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = DBContext.tblUser.Find(id);
            tblUser.SelectedPreferences = DBContext.tblUserPreference.Where(x=>x.UserId == tblUser.GUID).Select(p=>p.tblPreference).ToList();

            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(DBContext.tblRol, "Id", "Name", tblUser.RolId);
            return View(tblUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                DBContext.Entry(tblUser).State = EntityState.Modified;
                DBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(DBContext.tblRol, "Id", "Name", tblUser.RolId);
            return View(tblUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = DBContext.tblUser.Find(id);
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
            tblUser tblUser = DBContext.tblUser.Find(id);
            DBContext.tblUser.Remove(tblUser);
            DBContext.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DBContext.Dispose();
            }
            base.Dispose(disposing);
        }*/

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginObject loginObject)
        {
            if (loginObject != null && !string.IsNullOrEmpty(loginObject.UserName) && !string.IsNullOrEmpty(loginObject.PassWord))
            {

                tblUser loggedUser = DBContext.tblUser.Where(user => user.Email.Equals(loginObject.UserName) && user.Pass.Equals(loginObject.PassWord)).FirstOrDefault();
                if (loggedUser == null)
                {

                    ViewBag.Error = "Wrong Credentials...";
                    return View();
                }
                Session.Add("USER", loggedUser);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Error = "Login Information Missing";
                return View();
            }


        }



        public ActionResult LogOff()
        {
            if (Session["USER"] != null)
            {
                Session.Remove("USER");
            }


            return RedirectToAction("Index", "Home");
        }


    }
}
