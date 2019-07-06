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
    public class GIAO_VIENController : Controller
    {
        private QL_TRUNGTAMEntities1 db = new QL_TRUNGTAMEntities1();
        private BASE bASE = new BASE();
        // GET: Admin/GIAO_VIEN
        public ActionResult Index()
        {
            var gIAO_VIEN = db.GIAO_VIEN.Include(g => g.TAI_KHOAN);
            return View(gIAO_VIEN.ToList());
        }
        // GET: Admin/GIAO_VIEN/Create
        public ActionResult Create()
        {
            ViewBag.MA_GV = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU");
            return View();
        }

        // POST: Admin/GIAO_VIEN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_GV,HO_TEN,SDT,GIOI_TINH,EMAIL,NG_SINH")] GIAO_VIEN gIAO_VIEN)
        {
            if (ModelState.IsValid)
            {
                var ma_gv = db.GIAO_VIEN.Where(m => m.MA_GV == "1000000001");
                if (ma_gv == null)
                    gIAO_VIEN.MA_GV = "1000000001";
                else
                {
                    int ma = int.Parse(db.GIAO_VIEN.Select(m => m.MA_GV).ToList().Last()) + 1;
                    gIAO_VIEN.MA_GV = ma.ToString();
                }
                db.GIAO_VIEN.Add(gIAO_VIEN);
                bASE.create_TAI_KHOAN(gIAO_VIEN.MA_GV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gIAO_VIEN);
        }

        // GET: Admin/GIAO_VIEN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAO_VIEN gIAO_VIEN = db.GIAO_VIEN.Find(id);
            if (gIAO_VIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_GV = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU", gIAO_VIEN.MA_GV);
            return View(gIAO_VIEN);
        }

        // POST: Admin/GIAO_VIEN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_GV,HO_TEN,SDT,GIOI_TINH,EMAIL,NG_SINH")] GIAO_VIEN gIAO_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIAO_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_GV = new SelectList(db.TAI_KHOAN, "TAI_KHOAN1", "MAT_KHAU", gIAO_VIEN.MA_GV);
            return View(gIAO_VIEN);
        }

        // GET: Admin/GIAO_VIEN/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAO_VIEN gIAO_VIEN = db.GIAO_VIEN.Find(id);
            if (gIAO_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAO_VIEN);
        }

        // POST: Admin/GIAO_VIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GIAO_VIEN gIAO_VIEN = db.GIAO_VIEN.Find(id);
            db.GIAO_VIEN.Remove(gIAO_VIEN);
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            db.TAI_KHOAN.Remove(tAI_KHOAN);
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
