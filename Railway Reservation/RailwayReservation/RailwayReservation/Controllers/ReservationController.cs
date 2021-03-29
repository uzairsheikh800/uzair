using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RailwayReservation.Models;

namespace RailwayReservation.Controllers
{
    
    public class ReservationController : Controller
    {
        private RailwayEntities db = new RailwayEntities();

        // GET: Reservation
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.TrainDetail1).Include(r => r.TrainSchedule1);
            return View(reservation.ToList().Where(a => a.Name == Session["name"].ToString()));
        }
        public ActionResult Confirm()
        {
            var reservation = db.Reservation.Include(r => r.TrainDetail1).Include(r => r.TrainSchedule1);
            return View(reservation.ToList().Where(a => a.Name == Session["name"].ToString()));
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            ViewBag.TrainDetail = new SelectList(db.TrainDetail, "Id", "TrainName");
            ViewBag.TrainSchedule = new SelectList(db.TrainSchedule, "Id", "Start_Station");
            return View();
        }
        

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Journey_Date,Start_Station,End_Station,TrainDetail,TrainSchedule,Seats,Bill")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (reservation.TrainDetail == 1)
                {
                    reservation.Bill = 1000 * reservation.Seats;
                }
                else if (reservation.TrainDetail == 2)
                {
                    reservation.Bill = 2000 * reservation.Seats;
                }
                reservation.Name = Session["name"].ToString();

                db.Reservation.Add(reservation);
                db.SaveChanges();
                ConfirmSeat confirmSeat = new ConfirmSeat();
                confirmSeat.Name = reservation.Name;
                confirmSeat.Start_Station = reservation.Start_Station;
                confirmSeat.End_Station = reservation.End_Station;
                confirmSeat.Seats = reservation.Seats;
                confirmSeat.Journey_Date = reservation.Journey_Date;
                confirmSeat.Bill = reservation.Bill;
                confirmSeat.TrainDetail = reservation.TrainDetail;
                confirmSeat.TrainSchedule = reservation.TrainSchedule;
                Random r = new Random();
                int random = r.Next(1, 100);
                confirmSeat.Seat_No = random;
                confirmSeat.PRN = confirmSeat.Name + confirmSeat.Seat_No + confirmSeat.Bill;
                db.ConfirmSeat.Add(confirmSeat);
                db.SaveChanges();
                return RedirectToAction("Confirm");
            }

            ViewBag.TrainDetail = new SelectList(db.TrainDetail, "Id", "TrainName", reservation.TrainDetail);
            ViewBag.TrainSchedule = new SelectList(db.TrainSchedule, "Id", "Start_Station", reservation.TrainSchedule);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainDetail = new SelectList(db.TrainDetail, "Id", "TrainName", reservation.TrainDetail);
            ViewBag.TrainSchedule = new SelectList(db.TrainSchedule, "Id", "Start_Station", reservation.TrainSchedule);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Journey_Date,Start_Station,End_Station,TrainDetail,TrainSchedule,Seats,Bill")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainDetail = new SelectList(db.TrainDetail, "Id", "TrainName", reservation.TrainDetail);
            ViewBag.TrainSchedule = new SelectList(db.TrainSchedule, "Id", "Start_Station", reservation.TrainSchedule);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
