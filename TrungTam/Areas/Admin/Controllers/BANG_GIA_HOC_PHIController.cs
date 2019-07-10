using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using PagedList;
namespace TrungTam.Areas.Admin.Controllers
{
    public class BANG_GIA_HOC_PHIController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/BANG_GIA_HOC_PHI
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Include(b => b.KHOI_LOP).Include(b => b.LOAI_LOP).Include(b => b.MON_HOC);
            ViewBag.listkhoi = db.KHOI_LOP.OrderByDescending(m=>m.TEN_KHOI).ToList();
            ViewBag.listloailop = db.LOAI_LOP.OrderByDescending(m => m.TEN_LOAI).ToList();
            ViewBag.listmonhoc = db.MON_HOC.OrderByDescending(m => m.TEN_MON).ToList();
            return View(bANG_GIA_HOC_PHI.OrderByDescending(m=>m.DON_GIA).ToPagedList(page, pageSize));
        }

        // GET: Admin/BANG_GIA_HOC_PHI/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Find(id);
            if (bANG_GIA_HOC_PHI == null)
            {
                return HttpNotFound();
            }
            return View(bANG_GIA_HOC_PHI);
        }

        //// GET: Admin/BANG_GIA_HOC_PHI/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI");
        //    ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI");
        //    ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON");
        //    return View();
        //}

        // POST: Admin/BANG_GIA_HOC_PHI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MA_KHOI,MA_MON,MA_LOAI,DON_GIA,SO_BUOI")] BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bANG_GIA_HOC_PHI.MA_KHOI = Guid.NewGuid();
        //        db.BANG_GIA_HOC_PHI.Add(bANG_GIA_HOC_PHI);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", bANG_GIA_HOC_PHI.MA_KHOI);
        //    ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", bANG_GIA_HOC_PHI.MA_LOAI);
        //    ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", bANG_GIA_HOC_PHI.MA_MON);
        //    return View(bANG_GIA_HOC_PHI);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                BANG_GIA_HOC_PHI bhp = new BANG_GIA_HOC_PHI();
                bhp.MA_KHOI = Guid.Parse(f["makhoi"]);
                bhp.MA_LOAI = Guid.Parse(f["maloai"]);
                bhp.MA_MON = Guid.Parse(f["mamon"]);
                bhp.DON_GIA = decimal.Parse(f["dongia"]);
                bhp.SO_BUOI = float.Parse(f["sobuoi"]);
                db.BANG_GIA_HOC_PHI.Add(bhp);
                db.SaveChanges();
                return RedirectToAction("Index", "BANG_GIA_HOC_PHI", new { area = "Admin" });
            }
            return View();
        }
        // GET: Admin/BANG_GIA_HOC_PHI/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Find(id);
            if (bANG_GIA_HOC_PHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", bANG_GIA_HOC_PHI.MA_KHOI);
            ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", bANG_GIA_HOC_PHI.MA_LOAI);
            ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", bANG_GIA_HOC_PHI.MA_MON);
            return View(bANG_GIA_HOC_PHI);
        }

        // POST: Admin/BANG_GIA_HOC_PHI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_KHOI,MA_MON,MA_LOAI,DON_GIA,SO_BUOI")] BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANG_GIA_HOC_PHI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", bANG_GIA_HOC_PHI.MA_KHOI);
            ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", bANG_GIA_HOC_PHI.MA_LOAI);
            ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", bANG_GIA_HOC_PHI.MA_MON);
            return View(bANG_GIA_HOC_PHI);
        }

        // GET: Admin/BANG_GIA_HOC_PHI/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Find(id);
            if (bANG_GIA_HOC_PHI == null)
            {
                return HttpNotFound();
            }
            return View(bANG_GIA_HOC_PHI);
        }

        // POST: Admin/BANG_GIA_HOC_PHI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Find(id);
            db.BANG_GIA_HOC_PHI.Remove(bANG_GIA_HOC_PHI);
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
