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
            return View(bANG_GIA_HOC_PHI.OrderByDescending(m=>m.NGAY_AP_DUNG).ToPagedList(page, pageSize));
        }

  
        [HttpPost]
        public ActionResult Delete(string ngayap)
        {
            var ngay = DateTime.Parse(ngayap);
            var ngay_new = ngay.ToString("dd/MM/yyyy HH:mm:ss");
            var ngay_sudung = DateTime.Parse(ngay_new);
            var lOP_HOC = (from l in db.LOP_HOC
                          where l.NGAY_AP_DUNG == ngay_sudung
                          select l).Count();
            string a = "0";
            if(lOP_HOC == 0)
            {
                BANG_GIA_HOC_PHI bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Find(ngay_sudung);
                db.BANG_GIA_HOC_PHI.Remove(bANG_GIA_HOC_PHI);
                db.SaveChanges();
                a = "1";
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            return Json(a, JsonRequestBehavior.AllowGet);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                BANG_GIA_HOC_PHI bhp = new BANG_GIA_HOC_PHI();
                var ngayapdung = (DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                bhp.NGAY_AP_DUNG = DateTime.Parse(ngayapdung);
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
