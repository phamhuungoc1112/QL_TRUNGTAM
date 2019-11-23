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
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();

        // GET: Admin/BANG_GIA_HOC_PHI
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9')
            {
                return Redirect("/Home/Index");
            }
            var bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Include(b => b.KHOI_LOP).Include(b => b.LOAI_LOP).Include(b => b.MON_HOC);
            ViewBag.listkhoi = db.KHOI_LOP.OrderByDescending(m => m.TEN_KHOI).ToList();
            ViewBag.listloailop = db.LOAI_LOP.OrderByDescending(m => m.TEN_LOAI).ToList();
            ViewBag.listmonhoc = db.MON_HOC.OrderByDescending(m => m.TEN_MON).ToList();
            return View(bANG_GIA_HOC_PHI.OrderByDescending(m => m.MA_KHOI).ToPagedList(page, pageSize));
        }


        [HttpPost]
        public ActionResult Delete(string ngayap)
        {
            var ngay = DateTime.Parse(ngayap);
            var ngay_new = ngay.ToString("yyyy/MM/dd HH:mm:ss");
            var ngay_sudung = DateTime.Parse(ngay_new);
            var lOP_HOC = (from l in db.LOP_HOC
                           where l.NGAY_AP_DUNG == ngay_sudung
                           select l).Count();
            string a = "0";
            if (lOP_HOC == 0)
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
                string a = f["sobuoi"].Replace('.', ',');
                //if(a.Contains("."))
                //string kq = a[0] + ',' + a[1];
                bhp.SO_BUOI = float.Parse(a);
                db.BANG_GIA_HOC_PHI.Add(bhp);
                db.SaveChanges();
                return RedirectToAction("Index", "BANG_GIA_HOC_PHI", new { area = "Admin" });
            }
            return View();
        }
        //public ActionResult Details()
        //{
        //    if (string.IsNullOrEmpty(Request.QueryString["id"]))
        //    {
        //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DateTime a = DateTime.Parse(Request.QueryString["id"]);
        //    var bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Include(b => b.KHOI_LOP).Include(b => b.LOAI_LOP).Include(b => b.MON_HOC);
        //    ViewBag.listkhoi = db.KHOI_LOP.OrderByDescending(m => m.TEN_KHOI).ToList();
        //    ViewBag.listloailop = db.LOAI_LOP.OrderByDescending(m => m.TEN_LOAI).ToList();
        //    ViewBag.listmonhoc = db.MON_HOC.OrderByDescending(m => m.TEN_MON).ToList();
        //    return View(bANG_GIA_HOC_PHI.Where(t=>t.NGAY_AP_DUNG.Equals(a)).OrderByDescending(m => m.MA_KHOI).ToList());
        //}
        
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime a = DateTime.Parse(id);
            var bANG_GIA_HOC_PHI = db.BANG_GIA_HOC_PHI.Where(p => p.NGAY_AP_DUNG.Equals(a));
            ViewBag.listkhoi = db.KHOI_LOP.OrderByDescending(m => m.TEN_KHOI).ToList();
            ViewBag.listloailop = db.LOAI_LOP.OrderByDescending(m => m.TEN_LOAI).ToList();
            ViewBag.listmonhoc = db.MON_HOC.OrderByDescending(m => m.TEN_MON).ToList();
            if (bANG_GIA_HOC_PHI == null)
            {
                return HttpNotFound();
            }
            return View(bANG_GIA_HOC_PHI.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection f)
        {
            DateTime ax = DateTime.Parse(f["ngayad"]);
            BANG_GIA_HOC_PHI bhp = db.BANG_GIA_HOC_PHI.Find(ax);
            bhp.MA_KHOI = Guid.Parse(f["makhoi"]);
            bhp.MA_LOAI = Guid.Parse(f["maloai"]);
            bhp.MA_MON = Guid.Parse(f["mamon"]);
            bhp.DON_GIA = decimal.Parse(f["dongia"]);
            string a = f["sobuoi"].Replace('.', ',');
            bhp.SO_BUOI = float.Parse(a);
            db.SaveChanges();
            return RedirectToAction("Index", "BANG_GIA_HOC_PHI");
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
