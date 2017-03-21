using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IOUTrackerApp.Models;

namespace IOUTrackerApp.Controllers
{
    [Authorize]
    public class IOUStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IOUStatus
        public ActionResult Index()
        {
            var iOUStatus = db.IOUStatus.Include(i => i.color);
            return View(iOUStatus.ToList());
        }

        // GET: IOUStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IOUStatus iOUStatus = db.IOUStatus.Find(id);
            if (iOUStatus == null)
            {
                return HttpNotFound();
            }
            return View(iOUStatus);
        }

        // GET: IOUStatus/Create
        public ActionResult Create()
        {
            ViewBag.colorId = new SelectList(db.Colors, "id", "color");
            return View();
        }

        // POST: IOUStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statusID,status,colorId")] IOUStatus iOUStatus)
        {
            if (ModelState.IsValid)
            {
                db.IOUStatus.Add(iOUStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.colorId = new SelectList(db.Colors, "id", "color", iOUStatus.colorId);
            return View(iOUStatus);
        }

        // GET: IOUStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IOUStatus iOUStatus = db.IOUStatus.Find(id);
            if (iOUStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.colorId = new SelectList(db.Colors, "id", "color", iOUStatus.colorId);
            return View(iOUStatus);
        }

        // POST: IOUStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statusID,status,colorId")] IOUStatus iOUStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iOUStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.colorId = new SelectList(db.Colors, "id", "color", iOUStatus.colorId);
            return View(iOUStatus);
        }

        // GET: IOUStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IOUStatus iOUStatus = db.IOUStatus.Find(id);
            if (iOUStatus == null)
            {
                return HttpNotFound();
            }
            return View(iOUStatus);
        }

        // POST: IOUStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IOUStatus iOUStatus = db.IOUStatus.Find(id);
            db.IOUStatus.Remove(iOUStatus);
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
