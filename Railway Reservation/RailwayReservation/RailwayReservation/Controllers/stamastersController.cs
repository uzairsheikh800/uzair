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
    public class stamastersController : Controller
    {
        private RailwayEntities db = new RailwayEntities();

        // GET: stamasters
        public ActionResult Index()
        {
            return View(db.stamaster.ToList());
        }

        // GET: stamasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stamaster stamaster = db.stamaster.Find(id);
            if (stamaster == null)
            {
                return HttpNotFound();
            }
            return View(stamaster);
        }

        // GET: stamasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stamasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,DivisionName")] stamaster stamaster)
        {
            if (ModelState.IsValid)
            {
                db.stamaster.Add(stamaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stamaster);
        }

        // GET: stamasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stamaster stamaster = db.stamaster.Find(id);
            if (stamaster == null)
            {
                return HttpNotFound();
            }
            return View(stamaster);
        }

        // POST: stamasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,DivisionName")] stamaster stamaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stamaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stamaster);
        }

        // GET: stamasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stamaster stamaster = db.stamaster.Find(id);
            if (stamaster == null)
            {
                return HttpNotFound();
            }
            return View(stamaster);
        }

        // POST: stamasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            stamaster stamaster = db.stamaster.Find(id);
            db.stamaster.Remove(stamaster);
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
