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
        private UserdbContext userdb = new UserdbContext();
        private UserTravelDbContext userTravelDatadb = new UserTravelDbContext();
        private static int currentPlaceId = 0;
        private static int currentHotelId = 0;
        private static int currentTourId = 0;

        public ActionResult UserPage()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public ActionResult UserPage(string searchTerm)
        {

            if (Session["ID"] != null)
            {
                if (db.Place.Where(x => x.Name == searchTerm).Any() == true)
                {
                    Place place = db.Place.Where(x => x.Name == searchTerm).FirstOrDefault();
                    int id = place.ID;
                    return RedirectToAction("DetailsPlaceAndBookSeat", "User", new { id = id });
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "Sorry the content is not available");
                    return View();
                }

            }
            else
                return RedirectToAction("Login", "Home");
        }
        public JsonResult getPlace(string term)
        {
            List<string> placeList = db.Place.Where(x => x.Name.StartsWith(term)).Select(y => y.Name).ToList();
            return Json(placeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Sea(int? page, string sortBy)
        {
            if (Session["ID"] != null)
            {
                var place = db.Place.Where(x => x.Area == "Sea").ToList().AsQueryable();
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
        public ActionResult BookHotel(int? page,string sortBy)
        {

            if (Session["ID"] != null)
            {
                Place place = db.Place.Where(x => x.ID == currentPlaceId).FirstOrDefault();
                List<Hotel> hotels = db.Hotel.Where(x => x.Place == place.Name).ToList();
                if(sortBy == "Price")
                {
                    hotels = hotels.OrderBy(x => x.Price).ToList();
                }

                if(sortBy == "Rating")
                {
                    hotels = hotels.OrderByDescending(x => x.Rating).ToList();
                }

                return View(hotels.ToPagedList(page ?? 1, 6));
            }
            return RedirectToAction("Login", "Home");

        }

        public ActionResult BookHotelDetails(int id)
        {
            if (Session["ID"] != null)
            {
                Hotel hotel = db.Hotel.Where(x => x.ID == id).FirstOrDefault();
                currentHotelId = id;
                var base64 = Convert.ToBase64String(hotel.Photos);
                var imgSource = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.SourcePhoto = imgSource;
                return View(hotel);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult BookHotelDetails(string quantityChildren, string quantityAdults, DateTime checkInDate, DateTime checkOutDate,string quantityRooms)
        {
            if (Session["ID"] != null)
            {
                if (ModelState.IsValid)
                {
                    userTravelData usertraveldata = new userTravelData();
                    usertraveldata.adult = Convert.ToInt32(quantityAdults);
                    usertraveldata.children = Convert.ToInt32(quantityChildren);
                    usertraveldata.checkinDate = checkInDate;
                    usertraveldata.checkoutDate = checkOutDate;
                    usertraveldata.quantityRoom = Convert.ToInt32(quantityRooms);
                    Hotel hotel = db.Hotel.Where(x => x.ID == currentHotelId).FirstOrDefault();
                    Place place = db.Place.Where(x => x.ID == currentPlaceId).FirstOrDefault();
                    usertraveldata.place = place.Name.ToString();
                    usertraveldata.Hotel = hotel.Name.ToString();
                    string username = Session["Username"].ToString();
                    usertraveldata.username = username;
                    usertraveldata.payment = (Convert.ToInt32((checkOutDate - checkInDate).TotalDays) + 1) * Convert.ToInt32(hotel.Price);
                    userTravelDatadb.userTravelData.Add(usertraveldata);
                    userTravelDatadb.SaveChanges();
                    ModelState.Clear();
                }
                return RedirectToAction("UserPage", "User");
            }
            else
                return RedirectToAction("Login", "Home");

        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            if (Session["ID"] != null)
            {
                string username = Session["Username"].ToString();
                var user = userdb.userTravel.Where(x => x.Username == username).FirstOrDefault();
                return View(user);
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Username"] = null;
            return RedirectToAction("HomePage", "Home");
        }

        [HttpPost]
        public JsonResult addRatingPlaceTable(float rating)
        {
            Place currentRatedplace = db.Place.Where(x => x.ID == currentPlaceId).FirstOrDefault();
            string username = Session["Username"].ToString();
            if (!userTravelDatadb.userRatingPlace.Where(x => x.Username == username && x.Place == currentRatedplace.Name).Any())
            {
                //Alter userRatingPlace table
                userRatingPlace userRating = new userRatingPlace();
                userRating.Username = username;
                userRating.Place = currentRatedplace.Name;
                userRating.RatingPlace = rating;
                userTravelDatadb.userRatingPlace.Add(userRating);
                userTravelDatadb.SaveChanges();

                //Alter Place table
                currentRatedplace.totalRatings = currentRatedplace.totalRatings + 1;
                currentRatedplace.Rating = ((currentRatedplace.Rating) * (currentRatedplace.totalRatings - 1) + rating) / currentRatedplace.totalRatings;
                db.SaveChanges();
            }
            else
            {
                //Alter userRatingPlace table
                userRatingPlace userRated = userTravelDatadb.userRatingPlace.Where(x => x.Username == username && x.Place == currentRatedplace.Name).FirstOrDefault();
                userRated.RatingPlace = rating;
                userTravelDatadb.SaveChanges();

                //Alter Place table
                currentRatedplace.Rating = ((currentRatedplace.Rating) * (currentRatedplace.totalRatings) + rating - userRated.RatingPlace) / currentRatedplace.totalRatings;
                db.SaveChanges();
            }
            return Json("Response from Rating System");
        }

        [HttpPost]
        public JsonResult addRatingHotelTable(float rating)
        {
            Hotel currentRatedHotel = db.Hotel.Where(x => x.ID == currentHotelId).FirstOrDefault();
            string username = Session["Username"].ToString();
            if (!userTravelDatadb.userRatingHotel.Where(x => x.Username == username && x.Hotel == currentRatedHotel.Name).Any())
            {
                //Alter userRatingPlace table
                userRatingHotel userRating = new userRatingHotel();
                userRating.Username = username;
                userRating.Hotel = currentRatedHotel.Name;
                userRating.RatingHotel = rating;
                userTravelDatadb.userRatingHotel.Add(userRating);
                userTravelDatadb.SaveChanges();

                //Alter Place table
                currentRatedHotel.totalRatings = currentRatedHotel.totalRatings + 1;
                currentRatedHotel.Rating = ((currentRatedHotel.Rating) * (currentRatedHotel.totalRatings - 1) + rating) / currentRatedHotel.totalRatings;
                db.SaveChanges();
            }
            else
            {
                //Alter userRatingPlace table
                userRatingHotel userRated = userTravelDatadb.userRatingHotel.Where(x => x.Username == username && x.Hotel == currentRatedHotel.Name).FirstOrDefault();
                userRated.RatingHotel = rating;
                userTravelDatadb.SaveChanges();

                //Alter Place table
                currentRatedHotel.Rating = ((currentRatedHotel.Rating) * (currentRatedHotel.totalRatings) + rating - userRated.RatingHotel) / currentRatedHotel.totalRatings;
                db.SaveChanges();
            }
            return Json("Response from Rating System");
        }

        public ActionResult showTour()
        {
            if (Session["ID"] != null)
            {
                string username = Session["Username"].ToString();
                List<userTravelData> tours = userTravelDatadb.userTravelData.Where(x => x.username == username).ToList();
                return View(tours);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult EditTour(int id)
        {
            if (Session["ID"] != null)
            {
                currentTourId = id;
                userTravelData tour = userTravelDatadb.userTravelData.Where(x => x.ID == id).FirstOrDefault();
                return View(tour);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult EditTour(string quantityChildren, string quantityAdults,string quantityRooms, DateTime checkInDate, DateTime checkOutDate)
        {
            if (Session["ID"] != null)
            {
                if (ModelState.IsValid)
                {
                    userTravelData currentTour = userTravelDatadb.userTravelData.Where(x => x.ID == currentTourId).FirstOrDefault();
                    currentTour.children = Convert.ToInt32(quantityChildren);
                    currentTour.adult = Convert.ToInt32(quantityAdults);
                    currentTour.quantityRoom = Convert.ToInt32(quantityRooms);
                    currentTour.checkinDate = checkInDate;
                    currentTour.checkoutDate = checkOutDate;
                    Hotel currentBookedHotel = db.Hotel.Where(x => x.ID == currentHotelId).FirstOrDefault();
                    currentTour.payment = Convert.ToInt32((checkOutDate - checkInDate)) * Convert.ToInt32(currentBookedHotel.Price);
                    userTravelDatadb.SaveChanges();
                }
                return RedirectToAction("showTour","User");
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult DeleteTour(int id)
        {

            if (Session["ID"] != null)
            {
                userTravelData tour = userTravelDatadb.userTravelData.Where(x => x.ID == id).FirstOrDefault();
                return View(tour);
            }
            else
                return RedirectToAction("Login", "Home");
        }

        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTourConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                userTravelData tour = userTravelDatadb.userTravelData.Where(x => x.ID == id).FirstOrDefault();
                userTravelDatadb.userTravelData.Remove(tour);

                userTravelDatadb.SaveChanges();
            }
            return RedirectToAction("showTour","User");
        }



        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

    }
}