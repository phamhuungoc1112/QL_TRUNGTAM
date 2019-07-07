using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;

namespace TrungTam.Areas.Admin.Controllers
{
    public class KHOI_LOPController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();

        // GET: Admin/KHOI_LOP
        public ActionResult Index()
        {
            return View(db.KHOI_LOP.ToList());
        }

        // GET: Admin/KHOI_LOP/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOI_LOP kHOI_LOP = db.KHOI_LOP.Find(id);
            if (kHOI_LOP == null)
            {
                return HttpNotFound();
            }
            return View(kHOI_LOP);
        }

        // GET: Admin/KHOI_LOP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KHOI_LOP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_KHOI,TEN_KHOI")] KHOI_LOP kHOI_LOP)
        {
            if (ModelState.IsValid)
            {
                kHOI_LOP.MA_KHOI = Guid.NewGuid();
                db.KHOI_LOP.Add(kHOI_LOP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHOI_LOP);
        }

        // GET: Admin/KHOI_LOP/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOI_LOP kHOI_LOP = db.KHOI_LOP.Find(id);
            if (kHOI_LOP == null)
            {
                return HttpNotFound();
            }
            return View(kHOI_LOP);
        }

        // POST: Admin/KHOI_LOP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_KHOI,TEN_KHOI")] KHOI_LOP kHOI_LOP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHOI_LOP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHOI_LOP);
        }

        // GET: Admin/KHOI_LOP/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOI_LOP kHOI_LOP = db.KHOI_LOP.Find(id);
            if (kHOI_LOP == null)
            {
                return HttpNotFound();
            }
            return View(kHOI_LOP);
        }

        // POST: Admin/KHOI_LOP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            KHOI_LOP kHOI_LOP = db.KHOI_LOP.Find(id);
            db.KHOI_LOP.Remove(kHOI_LOP);
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
