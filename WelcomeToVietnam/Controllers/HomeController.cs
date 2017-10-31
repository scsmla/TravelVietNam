using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WelcomeToVietnam.Models;
namespace WelcomeToVietnam.Controllers
{
    public class HomeController : Controller
    {
        private UserdbContext db = new UserdbContext();

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                db.userTravel.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.message = user.Username + "registered success";
                return RedirectToAction("Login");
            }

            return View();

        }

        public JsonResult IsUserAvaiable(string Username)
        {
            return Json(!db.userTravel.Any(u => u.Username == Username), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.userTravel.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password,Gender,Age,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserPage");
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var usr = db.userTravel.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["Username"] = usr.Username.ToString();
                Session["ID"] = usr.ID.ToString();
                return RedirectToAction("UserPage","User");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
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