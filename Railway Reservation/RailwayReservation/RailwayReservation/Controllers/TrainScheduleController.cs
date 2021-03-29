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
    [Authorize]
    public class TrainScheduleController : Controller
    {
        private RailwayEntities db = new RailwayEntities();

        // GET: TrainSchedule
        public ActionResult Index()
        {
            var trainSchedule = db.TrainSchedule.Include(t => t.TrainDetail);
            return View(trainSchedule.ToList());
        }

        // GET: TrainSchedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainSchedule trainSchedule = db.TrainSchedule.Find(id);
            if (trainSchedule == null)
            {
                return HttpNotFound();
            }
            return View(trainSchedule);
        }

        // GET: TrainSchedule/Create
        public ActionResult Create()
        {
            ViewBag.Train_Id = new SelectList(db.TrainDetail, "Id", "TrainName");
            return View();
        }

        // POST: TrainSchedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Start_Station,Destination_Station,Arrival_Time,Departure_Time,Distance_Between,Train_Id")] TrainSchedule trainSchedule)
        {
            if (ModelState.IsValid)
            {
                db.TrainSchedule.Add(trainSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Train_Id = new SelectList(db.TrainDetail, "Id", "TrainName", trainSchedule.Train_Id);
            return View(trainSchedule);
        }

        // GET: TrainSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainSchedule trainSchedule = db.TrainSchedule.Find(id);
            if (trainSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Train_Id = new SelectList(db.TrainDetail, "Id", "TrainName", trainSchedule.Train_Id);
            return View(trainSchedule);
        }

        // POST: TrainSchedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Start_Station,Destination_Station,Arrival_Time,Departure_Time,Distance_Between,Train_Id")] TrainSchedule trainSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Train_Id = new SelectList(db.TrainDetail, "Id", "TrainName", trainSchedule.Train_Id);
            return View(trainSchedule);
        }

        // GET: TrainSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainSchedule trainSchedule = db.TrainSchedule.Find(id);
            if (trainSchedule == null)
            {
                return HttpNotFound();
            }
            return View(trainSchedule);
        }

        // POST: TrainSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainSchedule trainSchedule = db.TrainSchedule.Find(id);
            db.TrainSchedule.Remove(trainSchedule);
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
