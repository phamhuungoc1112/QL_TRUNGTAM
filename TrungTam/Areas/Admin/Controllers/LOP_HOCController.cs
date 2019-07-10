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
    public class LOP_HOCController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/LOP_HOC
        public ActionResult Index()
        {
            var lOP_HOC = db.LOP_HOC.Include(l => l.GIAO_VIEN).Include(l => l.KHOI_LOP).Include(l => l.LOAI_LOP).Include(l => l.MON_HOC);
            ViewBag.listkhoi = db.KHOI_LOP.OrderByDescending(m => m.TEN_KHOI).ToList();
            ViewBag.listloailop = db.LOAI_LOP.OrderByDescending(m => m.TEN_LOAI).ToList();
            ViewBag.listmonhoc = db.MON_HOC.OrderByDescending(m => m.TEN_MON).ToList();
            ViewBag.listgiaovien = db.GIAO_VIEN.OrderBy(m => m.HO_TEN).ToList();
            return View(lOP_HOC.ToList());
        }

        // GET: Admin/LOP_HOC/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            return View(lOP_HOC);
        }

        // GET: Admin/LOP_HOC/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN");
        //    ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI");
        //    ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI");
        //    ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON");
        //    return View();
        //}

        // POST: Admin/LOP_HOC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MA_LOP,TEN_LOP,SI_SO,MA_LOAI,MA_MON,MA_KHOI,MA_GV")] LOP_HOC lOP_HOC)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        lOP_HOC.MA_LOP = Guid.NewGuid();
        //        db.LOP_HOC.Add(lOP_HOC);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", lOP_HOC.MA_GV);
        //    ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", lOP_HOC.MA_KHOI);
        //    ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", lOP_HOC.MA_LOAI);
        //    ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", lOP_HOC.MA_MON);
        //    return View(lOP_HOC);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                LOP_HOC lh = new LOP_HOC();
                lh.MA_LOP = Guid.NewGuid();
                lh.MA_KHOI = Guid.Parse(f["makhoi"]);
                lh.MA_LOAI = Guid.Parse(f["maloai"]);
                lh.MA_MON = Guid.Parse(f["mamon"]);
                lh.MA_GV = f["magiaovien"];
                lh.TEN_LOP = f["tenlop"];
                lh.SI_SO = 0;
                db.LOP_HOC.Add(lh);
                db.SaveChanges();
                return RedirectToAction("Index", "LOP_HOC", new { area = "Admin" });
            }
            return View();
        }
        // GET: Admin/LOP_HOC/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", lOP_HOC.MA_GV);
            ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", lOP_HOC.MA_KHOI);
            ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", lOP_HOC.MA_LOAI);
            ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", lOP_HOC.MA_MON);
            return View(lOP_HOC);
        }

        // POST: Admin/LOP_HOC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_LOP,TEN_LOP,SI_SO,MA_LOAI,MA_MON,MA_KHOI,MA_GV")] LOP_HOC lOP_HOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOP_HOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", lOP_HOC.MA_GV);
            ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", lOP_HOC.MA_KHOI);
            ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", lOP_HOC.MA_LOAI);
            ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", lOP_HOC.MA_MON);
            return View(lOP_HOC);
        }

        // GET: Admin/LOP_HOC/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            return View(lOP_HOC);
        }

        // POST: Admin/LOP_HOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            db.LOP_HOC.Remove(lOP_HOC);
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
