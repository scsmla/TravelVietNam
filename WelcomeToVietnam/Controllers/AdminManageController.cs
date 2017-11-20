using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
                return RedirectToAction("HomePage", "AdminManage");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserAdmin"] = null;
            Session["ID"] = null;
            return RedirectToAction("Login", "AdminManage");
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
            if (Session["UserAdmin"] != null)
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase file = Request.Files["imageData"];
                    string path = Path.Combine(Server.MapPath("~/Photos"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    place.Photos = imageArray;
                    place.totalRatings = 0;
                    place.Rating = 0;
                    db.Place.Add(place);
                    db.SaveChanges();
                    return RedirectToAction("IndexPlace");
                }

                ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling", place.Area);
                return View(place);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        // GET: AdminManage/Edit/5
        public ActionResult EditPlace(int id)
        {

            Place place = db.Place.Where(x => x.ID == id).FirstOrDefault();
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
            if (Session["UserAdmin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(place).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexPlace");
                }
                ViewBag.Area = new SelectList(db.Area, "AreaTravelling", "AreaTravelling", place.Area);
                return View(place);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        // GET: AdminManage/Delete/5
        public ActionResult DeletePlace(int id)
        {

            Place place = db.Place.Where(x => x.ID == id).FirstOrDefault();
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

        [HttpPost, ActionName("DeletePlace")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlaceConfirmed(int id)
        {
            if (Session["UserAdmin"] != null)
            {
                Place place = db.Place.Where(x => x.ID == id).FirstOrDefault();
                List<Hotel> hotels = db.Hotel.Where(x => x.Place == place.Name).ToList();

                foreach (Hotel hotel in hotels)
                    db.Hotel.Remove(hotel);
                db.Place.Remove(place);

                db.SaveChanges();
                return RedirectToAction("IndexPlace");
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult CreateHotel()
        {
            if (Session["UserAdmin"] != null)
            {
                ViewBag.Place = new SelectList(db.Place, "Name", "Name");
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
        public ActionResult CreateHotel([Bind(Include = "Name,Place,Address,Photos,PhoneNumber,Price")] Hotel hotel)
        {
            if (Session["UserAdmin"] != null)
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase file = Request.Files["imageData"];
                    string path = Path.Combine(Server.MapPath("~/Photos"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    hotel.Photos = imageArray;
                    hotel.totalRatings = 0;
                    hotel.Rating = 0;
                    db.Hotel.Add(hotel);
                    db.SaveChanges();
                    return RedirectToAction("IndexHotel");
                }
                return View(hotel);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        [HttpGet]
        public ActionResult EditHotel(int id)
        {
            Hotel hotel = db.Hotel.Where(x => x.ID == id).FirstOrDefault();
            if (hotel == null)
            {
                return HttpNotFound();
            }
            if (Session["UserAdmin"] != null)
            {
                return View(hotel);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHotel([Bind(Include = "Name,Address,PhoneNumber")] Hotel hotel)
        {
            if (Session["UserAdmin"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hotel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("IndexHotel");
                }

                return View(hotel);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        public ActionResult DeleteHotel(int id)
        {

            Hotel hotel = db.Hotel.Where(x => x.ID == id).FirstOrDefault();
            if (hotel == null)
            {
                return HttpNotFound();
            }
            if (Session["UserAdmin"] != null)
            {
                return View(hotel);
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        [HttpPost, ActionName("DeleteHotel")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHotelConfirmed(int id)
        {
            if (Session["UserAdmin"] != null)
            {
                Hotel hotel = db.Hotel.Where(x => x.ID == id).FirstOrDefault();

                db.Hotel.Remove(hotel);

                db.SaveChanges();
                return RedirectToAction("IndexHotel");
            }
            else
                return RedirectToAction("Login", "AdminManage");
        }

        
    }
}
