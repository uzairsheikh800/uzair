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
    public class TrainDetailController : Controller
    {
        private RailwayEntities db = new RailwayEntities();

        // GET: TrainDetail
        public ActionResult Index()
        {
            return View(db.TrainDetail.ToList());
        }
        public ActionResult DayWiseTrain()
        {
            return View(db.TrainDetail.ToList());
        }
        [HttpPost]
        public ActionResult DayWiseTrain(string abc)
        {
            if (string.IsNullOrEmpty(abc) == true)
            {
                return PartialView("SearchPartialView", db.TrainDetail.ToList());
            }
            else
            {
                var searchresult = db.TrainDetail.Where(a => a.TrainName.Contains(abc)).ToList();
                return PartialView("SearchPartialView", searchresult);
            }
           // return View(db.TrainDetail.ToList());
        }


        // GET: TrainDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainDetail trainDetail = db.TrainDetail.Find(id);
            if (trainDetail == null)
            {
                return HttpNotFound();
            }
            return View(trainDetail);
        }

        // GET: TrainDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrainName,Status,RouteId,No_of_compartment,C1AC,C2AC,C3AC,Sleeper,General")] TrainDetail trainDetail)
        {
            if (ModelState.IsValid)
            {
                db.TrainDetail.Add(trainDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainDetail);
        }

        // GET: TrainDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainDetail trainDetail = db.TrainDetail.Find(id);
            if (trainDetail == null)
            {
                return HttpNotFound();
            }
            return View(trainDetail);
        }

        // POST: TrainDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrainName,Status,RouteId,No_of_compartment,C1AC,C2AC,C3AC,Sleeper,General")] TrainDetail trainDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainDetail);
        }

        // GET: TrainDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainDetail trainDetail = db.TrainDetail.Find(id);
            if (trainDetail == null)
            {
                return HttpNotFound();
            }
            return View(trainDetail);
        }

        // POST: TrainDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainDetail trainDetail = db.TrainDetail.Find(id);
            db.TrainDetail.Remove(trainDetail);
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
