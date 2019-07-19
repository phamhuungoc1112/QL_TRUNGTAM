using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Abstracts;
using System.Web.Script.Serialization;
namespace TrungTam.Areas.Admin.Controllers
{
    public class LOP_HOCController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/LOP_HOC
        public ActionResult Index()
        {
            var lOP_HOC = db.LOP_HOC.Include(l => l.GIAO_VIEN).Include(l => l.KHOI_LOP).Include(l => l.LOAI_LOP).Include(l => l.MON_HOC);
            var khoi = from a in db.KHOI_LOP
                       join b in db.BANG_GIA_HOC_PHI
                       on a.MA_KHOI equals b.MA_KHOI
                       select new HocPhi_Khoi()
                       {
                           MA_KHOI = a.MA_KHOI,
                           TEN_KHOI = a.TEN_KHOI
                       };
            ViewBag.listkhoi = khoi.Distinct().ToList();
            var loailop = from a in db.LOAI_LOP
                          join b in db.BANG_GIA_HOC_PHI
                          on a.MA_LOAI equals b.MA_LOAI
                          select new HocPhi_LoaiLop()
                          {
                              MA_LOAI = a.MA_LOAI,
                              TEN_LOAI = a.TEN_LOAI
                          };
            ViewBag.listloailop = loailop.ToList();
            var monhoc = from a in db.MON_HOC
                         join b in db.BANG_GIA_HOC_PHI
                         on a.MA_MON equals b.MA_MON
                         select new HocPhi_MonHoc()
                         {
                             MA_MON = a.MA_MON,
                             TEN_MON = a.TEN_MON
                         };
            var banghp = from a in db.BANG_GIA_HOC_PHI
                         join b in db.MON_HOC
                         on a.MA_MON equals b.MA_MON
                         join c in db.LOAI_LOP
                         on a.MA_LOAI equals c.MA_LOAI
                         join d in db.KHOI_LOP
                         on a.MA_KHOI equals d.MA_KHOI
                         select new BangHP_Khoi_Loai_Mon{
                             MA_KHOI = d.MA_KHOI,
                             TEN_KHOI = d.TEN_KHOI,
                             MA_MON = b.MA_MON,
                             TEN_MON = b.TEN_MON,
                             MA_LOAI = c.MA_LOAI,
                             TEN_LOAI= c.TEN_LOAI,                           
                             DON_GIA = a.DON_GIA,
                             SO_BUOI = a.SO_BUOI
                            };
            ViewBag.listmonhoc = monhoc.ToList();
            ViewBag.listgiaovien = db.GIAO_VIEN.OrderBy(m => m.HO_TEN).ToList();
            ViewBag.listbanghp = banghp.ToList();
            ViewBag.listHocSinh = (from a in db.HOC_SINH
                                  select a).ToList();
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
        //GET: Admin/LOP_HOC/Ajax_Loc
        //public JsonResult Ajax_Loc(string Khoi, string GoiY)
        //{
        //    Guid khoi = Guid.Parse(Khoi);
        //    if (GoiY == "1")
        //    {
                
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
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
        [AllowAnonymous]
        public JsonResult Create(string l)
        {
            //var a = f.makhoi;
            //var js = new JavaScriptSerializer();
            //var banghp = (Lop_Hoc)js.Deserialize(Json, typeof(banghpJson));
            if (ModelState.IsValid)
            {
                LOP_HOC lh = new LOP_HOC();
                lh.MA_LOP = Guid.NewGuid();
                //lh.MA_KHOI = Guid.Parse(l.makhoi);
                //lh.MA_MON = Guid.Parse(l.mamon);
                //lh.MA_LOAI = Guid.Parse(l.maloai);
                //lh.MA_GV = l.magiaovien;
                //lh.TEN_LOP = l.tenlop;
                db.LOP_HOC.Add(lh);
                db.SaveChanges();
                return Json(l);
            }
            return Json(l);
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
