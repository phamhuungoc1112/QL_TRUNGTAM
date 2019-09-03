using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Controllers;
using TrungTam.Areas.Admin.Models;
using TrungTam.Function_Base;
using TrungTam.Areas.Admin.Abstracts;

namespace TrungTam.Areas.Admin.Controllers
{
    public class TRANG_CHUController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        public ActionResult Index()
        {
            var noidung = db.TRANG_CHU.OrderByDescending(p => p.NGAY_AP_DUNG);
            ViewBag.noidung = noidung.ToList();
            var khuyenmai = db.KHUYEN_MAI.OrderBy(p => p.SO_MON_DK);
            ViewBag.khuyenmai = khuyenmai.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult update(FormCollection f)
        {
            TRANG_CHU trangchu = new TRANG_CHU();
            trangchu.NGAY_AP_DUNG = DateTime.Now;
            trangchu.MUC_TIEU = f["muctieu"];
            trangchu.GIOI_THIEU = f["gioithieu"];
            trangchu.DIA_CHI = f["diachi"];
            trangchu.EMAIL = f["email"];
            trangchu.SDT1 = f["sdt1"];
            trangchu.SDT2 = f["sdt2"];
            db.TRANG_CHU.Add(trangchu);
            db.SaveChanges();
            return RedirectToAction("Index", "TRANG_CHU", new { area = "Admin" });
        }
    }
}