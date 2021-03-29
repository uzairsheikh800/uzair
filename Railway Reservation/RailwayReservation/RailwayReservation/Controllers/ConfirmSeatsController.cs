using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using RailwayReservation.Models;

namespace RailwayReservation.Controllers
{
    
    public class ConfirmSeatsController : Controller
    {
        private RailwayEntities db = new RailwayEntities();

        // GET: ConfirmSeats
        public ActionResult Index()
        {
            return View(db.ConfirmSeat.ToList().Where(a => a.Name == Session["name"].ToString()));
        }

        // GET: ConfirmSeats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfirmSeat confirmSeat = db.ConfirmSeat.Find(id);
            if (confirmSeat == null)
            {
                return HttpNotFound();
            }
            return View(confirmSeat);
        }

        // GET: ConfirmSeats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfirmSeats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Journey_Date,Start_Station,End_Station,TrainDetail,TrainSchedule,Seats,Bill,Seat_No,PRN")] ConfirmSeat confirmSeat)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }

            return View(confirmSeat);
        }

        // GET: ConfirmSeats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfirmSeat confirmSeat = db.ConfirmSeat.Find(id);
            if (confirmSeat == null)
            {
                return HttpNotFound();
            }
            return View(confirmSeat);
        }

        // POST: ConfirmSeats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Journey_Date,Start_Station,End_Station,TrainDetail,TrainSchedule,Seats,Bill,Seat_No,PRN")] ConfirmSeat confirmSeat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confirmSeat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(confirmSeat);
        }

        // GET: ConfirmSeats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfirmSeat confirmSeat = db.ConfirmSeat.Find(id);
            if (confirmSeat == null)
            {
                return HttpNotFound();
            }
            return View(confirmSeat);
        }

        // POST: ConfirmSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConfirmSeat confirmSeat = db.ConfirmSeat.Find(id);
            db.ConfirmSeat.Remove(confirmSeat);
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
