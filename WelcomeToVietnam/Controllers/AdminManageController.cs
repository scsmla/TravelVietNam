using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WelcomeToVietnam.Models;

namespace WelcomeToVietnam.Controllers
{
    public class AdminManageController : Controller
    {
        private AreadbContext db = new AreadbContext();
        private AdminDbContext adminDb = new AdminDbContext();
        // GET: AdminManage
        public ActionResult IndexPlace()
        {
            if (Session["UserAdmin"] != null)
            {
                List<Place> listPlace = db.Place.ToList();
                return View(listPlace);
            }
            else
                return RedirectToAction("Login", "AdminManage");

        }
        public ActionResult IndexHotel()
        {
            if (Session["UserAdmin"] != null)
            {
                List<Hotel> listHotel = db.Hotel.ToList();
                return View(listHotel);
            }
            else
                return RedirectToAction("Login", "AdminManage");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblAdmin admin)
        {
            var ad = adminDb.tblAdmin.Where(x => x.UserName == admin.UserName && x.Password == admin.Password).FirstOrDefault();
            if (ad != null)
            {
                Session["UserAdmin"] = ad.UserName.ToString();
                Session["ID"] = ad.ID.ToString();
                return RedirectToAction("HomePage","AdminManage");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
        }

        public ActionResult HomePage()
        {
            if (Session["UserAdmin"] != null)
                return View();
            else
                return RedirectToAction("Login", "AdminManage");
        }
        // GET: AdminManage/Details/5
       

        // GET: AdminManage/Create
        public ActionResult CreatePlace()
        {
            if (Session["UserAdmin"] != null)
            { 
                ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling");
                return View();
            }
            else
                return RedirectToAction("Login", "AdminManage"); 
        }

        // POST: AdminManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlace([Bind(Include = "Name,Location,Area,Photos,CurrentStatus,VisitorsPerYear")] Place place)
        {
            if (ModelState.IsValid)
            {
                place.totalRatings = 0;
                place.Rating = 0;
                db.Place.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling", place.Area);
            return View(place);
        }

        // GET: AdminManage/Edit/5
        public ActionResult EditPlace(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Place.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            if (Session["UserAdmin"] != null)
            {
                ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling", place.Area);
                return View(place);
            }
            else
                return RedirectToAction("Login", "AdminManage");

        }

        // POST: AdminManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPlace([Bind(Include = "Name,Location,Area,Photos,CurrentStatus,VisitorsPerYear")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling", place.Area);
            return View(place);
        }

        // GET: AdminManage/Delete/5
        public ActionResult DeletePlace(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Place.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            if (Session["UserAdmin"] != null)
            {
                return View(place);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Place place = db.Place.Find(id);
            db.Place.Remove(place);
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
