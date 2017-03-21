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
    public class IOUsJQueryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AjaxIOUs/Create
        public ActionResult LoadPage()
        {
            ViewBag.statusId = new SelectList(db.IOUStatus, "statusID", "status");
            //return View();

            var iOU = new IOU { userID = User.Identity.Name, takenDt = DateTime.Today, statusChangedDt = DateTime.Today, statusId = db.IOUStatus.FirstOrDefault().statusID };

            var iOUs = db.IOUs
              .Where(i => i.userID == User.Identity.Name)
              .Include(i => i.status)
              .Include(i => i.status.color);
            var x = new IOUViewModel { iOU = iOU, allIOUsOfUser = iOUs.ToList() };
            x.allIOUStatuses = db.IOUStatus.ToList();
            return View(x);
        }

        [HttpGet]
        public ActionResult Display(int id)
        {
            var book = db.IOUs.Find(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int IOUid)
        {
            IOU iOU = db.IOUs.Find(IOUid);
            db.IOUs.Remove(iOU);
            db.SaveChanges();
            //return RedirectToAction("Index");

            //ViewBag.statusId = new SelectList(db.IOUStatus, "statusID", "status", iOU.statusId);

            //var iOUs = db.IOUs
            // .Where(i => i.userID == User.Identity.Name)
            // .Include(i => i.status)
            // .Include(i => i.status.color);
            //var x = new IOUViewModel { iOU = null, allIOUsOfUser = iOUs.ToList() };
            //x.allIOUStatuses = db.IOUStatus.ToList();
            return new HttpStatusCodeResult(HttpStatusCode.OK);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "IOUId,lenderName ,amount ,statusId ,plannedReturnDt")]IOU iOU)
        {
            if (ModelState.IsValid)
            {
                var _iou = db.IOUs.Find(iOU.IOUId);
                _iou.statusId = iOU.statusId;
                if (_iou.statusId != iOU.statusId)
                    _iou.statusChangedDt = DateTime.Now;
                _iou.plannedReturnDt = iOU.plannedReturnDt;
                _iou.lenderName = iOU.lenderName;
                _iou.amount = iOU.amount;

                iOU = _iou;
                db.Entry(iOU).State = EntityState.Modified;
                db.SaveChanges();
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "lenderName ,amount ,statusId ,plannedReturnDt")]IOU iou)
        {
            if (ModelState.IsValid)
            {
                iou.takenDt = DateTime.Today;
                iou.statusChangedDt = DateTime.Today;
                iou.userID = User.Identity.Name;

                db.IOUs.Add(iou);
                db.SaveChanges();
                return Json(iou);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }



        [HttpGet]
        public ActionResult GetAllIOUs()
        {
            var allIOUs = db.IOUs
                .Where(i => i.userID == User.Identity.Name)
                .Include(i => i.status)
                .Include(i => i.status.color);
            return Json(allIOUs, JsonRequestBehavior.AllowGet);
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
