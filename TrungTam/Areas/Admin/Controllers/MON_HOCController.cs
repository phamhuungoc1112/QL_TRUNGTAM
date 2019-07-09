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
    public class MON_HOCController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/MON_HOC
        public ActionResult Index()
        {
            return View(db.MON_HOC.ToList());
        }

        // GET: Admin/MON_HOC/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_HOC mON_HOC = db.MON_HOC.Find(id);
            if (mON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(mON_HOC);
        }

        // GET: Admin/MON_HOC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MON_HOC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_MON,TEN_MON")] MON_HOC mON_HOC)
        {
            if (ModelState.IsValid)
            {
                mON_HOC.MA_MON = Guid.NewGuid();
                db.MON_HOC.Add(mON_HOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mON_HOC);
        }

        // GET: Admin/MON_HOC/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_HOC mON_HOC = db.MON_HOC.Find(id);
            if (mON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(mON_HOC);
        }

        // POST: Admin/MON_HOC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_MON,TEN_MON")] MON_HOC mON_HOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mON_HOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mON_HOC);
        }

        // GET: Admin/MON_HOC/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_HOC mON_HOC = db.MON_HOC.Find(id);
            if (mON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(mON_HOC);
        }

        // POST: Admin/MON_HOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MON_HOC mON_HOC = db.MON_HOC.Find(id);
            db.MON_HOC.Remove(mON_HOC);
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
