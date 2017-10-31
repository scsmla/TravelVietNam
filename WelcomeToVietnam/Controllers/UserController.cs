using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WelcomeToVietnam.Models;
using System.Data.Entity;
using System.Web.UI.WebControls;
using PagedList;
using PagedList.Mvc;

namespace WelcomeToVietnam.Controllers
{
    public class UserController : Controller
    {
        private AreadbContext db = new AreadbContext();
        private static int currentPlaceId = 0; 
        public ActionResult UserPage()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult UserPage(string searchBy,string search)
        {

            if (Session["ID"] != null)
            {
                if (searchBy == "Area")
                {
                    if (db.Area.Where(x => x.AreaTravelling == search).Any() == true)
                        return RedirectToAction(search,"User");
                    else
                    {

                    }
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("", "Sorry the content is not available");
                        return View();
                    }
                }
                else 
                {
                    if (db.Place.Where(x => x.Name == search).Any() == true)
                    {
                        Place place = db.Place.Where(x => x.Name == search).FirstOrDefault();
                        
                        return RedirectToAction("DetailsPlaceAndBookSeat", "User",new { id = place.ID });
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("", "Sorry the content is not available");
                        return View();
                    }
                }
            }
            else
                return RedirectToAction("Login", "Home");
        }
        public JsonResult getPlace(string search)
        {
            List<string> placeList = db.Place.Where(x => x.Name.StartsWith(search)).Select(x => x.Name).ToList();
            return Json(placeList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Sea(int ?page,string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "Sea").ToList().AsQueryable();
                if(sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1 , 6));
            }
            return RedirectToAction("Login", "Home");
        }
       
        [HttpGet]
        public ActionResult NationalPark(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "NationalPark").ToList().AsQueryable();
                if (sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Relics(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "Relics").ToList().AsQueryable();
                if (sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Pagoda(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "Pagoda").ToList().AsQueryable();
                if (sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Moutain(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "Moutain").ToList().AsQueryable();
                if (sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult City(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "City").ToList().AsQueryable();
                if (sortBy == "VisitorPerYear")
                {
                    place = place.OrderByDescending(x => x.VisitorsPerYear);
                }
                if (sortBy == "Rating")
                {
                    place = place.OrderByDescending(x => x.Rating);
                }
                return View(place.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");
        }

       [HttpGet]
       public ActionResult DetailsPlaceAndBookSeat(int id)
       {

            if (Session["ID"] != null)
            {
               
                Place place = db.Place.Where(x => x.ID == id).FirstOrDefault();
                var base64 = Convert.ToBase64String(place.Photos);
                var imgSource = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.SourcePhoto = imgSource;
                currentPlaceId = id;
                return View(place);
            
            }
            return RedirectToAction("Login", "Home");
       }
        [HttpGet]
        public ActionResult BookHotel()
        {
                   
                Place place = db.Place.Where(x => x.ID == currentPlaceId).FirstOrDefault();
                List<Hotel> hotels = db.Hotel.Where(x => x.Place == place.Name).ToList();
                return View(hotels);
           
        }
        public ActionResult BookHotelDetails()
        {
            return View();
        }
    }
}