using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.DAL;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class TripsController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Trips
        public ActionResult Index()
        {
            var trips = db.Trips.ToList();
            return View(trips);
        }

        // GET: Trips/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profil profil = db.Profiles.Single(p => p.Login == User.Identity.Name);
            Trip trip = db.Trips.Find(id);
            if (!db.Reservations.Any(r => r.TripId == id && r.ProfilId == profil.ID))
            {
                trip.IsReserved = false;
                db.SaveChanges();
            }
            else
            {
                trip.IsReserved = true;
                db.SaveChanges();
            }
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        public ActionResult Reserved(int id)
        {
            Profil profil = db.Profiles.Single(p => p.Login == User.Identity.Name);
            Reservation reservation = new Reservation();
            Trip trip = db.Trips.Find(id);
            reservation.ProfilId = profil.ID; 
            reservation.Trip = trip;
            if (!db.Reservations.Any(r => r.TripId == id && r.ProfilId == profil.ID))
            {
                trip.Number--;
                trip.IsReserved = true;
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Details", "Trips", new { id = trip.ID });

            }

            return RedirectToAction("Details", "Trips", new { id = trip.ID });
        }
        public ActionResult DeleteReservation(int id, int Tripid)
        {
            Profil profil = db.Profiles.Single(p => p.Login == User.Identity.Name);
            Reservation reservation = profil.Reservations.Find(r => r.ID == id);
            Trip trip = db.Trips.Find(Tripid);
            reservation.Trip = trip;

            trip.Number++;
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index", "Reservations");
        }

 

        [Authorize(Roles = "Admin")]
        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Name,Photo,From,To,NumberOfPlaces,Number, IsReserved")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["fileOfPhoto"];
                if (file != null && file.ContentLength > 0)
                {
                    trip.Photo = file.FileName;
                    string s = HttpContext.Server.MapPath("~/Photo/") + trip.Photo;

                    file.SaveAs(s);
                }
                trip.Number = trip.NumberOfPlaces;
                trip.IsReserved = false;
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Trips/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Name,Photo,From,To,NumberOfPlaces,Number")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                HttpPostedFileBase file = Request.Files["fileOfPhoto"];
                if (file != null && file.ContentLength > 0)
                {
                    trip.Photo = file.FileName;
                    string s = HttpContext.Server.MapPath("~/Photo/") + trip.Photo;

                    file.SaveAs(s);
                }
                trip.Number = trip.NumberOfPlaces;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(long id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
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
