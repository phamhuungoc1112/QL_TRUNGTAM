using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Function_Base;

namespace TrungTam.Areas.Admin.Controllers
{
    public class HOC_SINHController : Controller
    {
        private QL_TRUNGTAMEntities1 db = new QL_TRUNGTAMEntities1();
        private BASE bASE = new BASE();
        // GET: Admin/HOC_SINH
        public ActionResult Index()
        {
            var hOC_SINH = db.HOC_SINH.Include(h => h.TAI_KHOAN);
            return View(hOC_SINH.ToList());
        }

        // GET: Admin/HOC_SINH/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            if (hOC_SINH == null)
            {
                return HttpNotFound();
            }
            return View(hOC_SINH);
        }

        // GET: Admin/HOC_SINH/Create
        public ActionResult Create()
        {
            ViewBag.MA_HS = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU");
            return View();
        }

        // POST: Admin/HOC_SINH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_HS,HO_TEN,KHOI,TRUONG,SDT,PHU_HUYNH")] HOC_SINH hOC_SINH)
        {
            if (ModelState.IsValid)
            {
                var ma_hs = db.HOC_SINH.Find("2000000001");
                if (ma_hs != null)
                {
                    long ma = long.Parse(db.HOC_SINH.Select(m => m.MA_HS).ToList().Last()) + 1;
                    hOC_SINH.MA_HS = ma.ToString();
                }
                else
                {
                    hOC_SINH.MA_HS = "2000000001";
                }
                db.HOC_SINH.Add(hOC_SINH);
                bASE.create_TAI_KHOAN(hOC_SINH.MA_HS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_HS = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU", hOC_SINH.MA_HS);
            return View(hOC_SINH);
        }

        // GET: Admin/HOC_SINH/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            if (hOC_SINH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_HS = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU", hOC_SINH.MA_HS);
            return View(hOC_SINH);
        }

        // POST: Admin/HOC_SINH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_HS,HO_TEN,KHOI,TRUONG,SDT,PHU_HUYNH")] HOC_SINH hOC_SINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOC_SINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_HS = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU", hOC_SINH.MA_HS);
            return View(hOC_SINH);
        }

        // GET: Admin/HOC_SINH/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            if (hOC_SINH == null)
            {
                return HttpNotFound();
            }
            return View(hOC_SINH);
        }

        // POST: Admin/HOC_SINH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            db.HOC_SINH.Remove(hOC_SINH);
            TAI_KHOAN x = db.TAI_KHOAN.Find(id);
            db.TAI_KHOAN.Remove(x);
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
