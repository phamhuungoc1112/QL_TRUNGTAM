﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Abstracts;

namespace TrungTam.Areas.Admin.Controllers
{
    public class THONG_KEController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        // GET: Admin/THONG_KE
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var year = DateTime.Now.Year;
            var hoadon = db.HOA_DON.Where(p => p.NGAY_THANH_TOAN.Year.Equals(year));
            var thongke_luong = (from p in hoadon
                                 group p by p.NGAY_THANH_TOAN.Month into hdgroup
                                 select new ThongKe_Luong
                                 {
                                     thang = hdgroup.Key,
                                     luong = hdgroup.Sum(item => item.TONG_TIEN)
                                 }).OrderByDescending(p => p.thang);
            if (thongke_luong.Count() != 0)
                ViewBag.thongke_luong = thongke_luong.ToList();
            else
                ViewBag.thongke_luong = 0;
            //----------------------------------------------
            var dem = db.CONG_NO.Count();
            
            if (dem != 0)
            {
                var congno = db.CONG_NO.Where(p => p.NGAY_LAP_CONG_NO.Year.Equals(year));
                var thongke_hocphi = (from p in congno
                                      group p by p.NGAY_LAP_CONG_NO.Month into hdgroup
                                      select new ThongKe_HocPhi
                                      {
                                          thang = hdgroup.Key,
                                          luong = hdgroup.Sum(item => item.TONG_TIEN)
                                      }).OrderByDescending(p => p.thang);
                if (thongke_hocphi.Count() != 0)
                    ViewBag.thongke_hocphi = thongke_hocphi.ToList();
                else
                    ViewBag.thongke_hocphi = 0;
            }
            //----------------------------------------------
            int hocvien = db.HOC_SINH.Where(p => p.TINH_TRANG == true).Count();
            ViewBag.tonghocvien = hocvien;
            //----------------------------------------------
            //----------------------------------------------
            var ghidanh = db.GHI_DANH;
            int num = 0;
            foreach (var item in ghidanh)
            {
                if (item.TINH_TRANG == false)
                    num += 1;
            }
            ViewBag.ghidanh = ghidanh.ToList();
            ViewBag.num = num;
            var thang = DateTime.Now.Month;
            int hocvienmoi = db.HOC_SINH.Where(p => p.NG_VAO_HOC.Value.Month.Equals(thang)).Count();
            ViewBag.hocvienmoi = hocvienmoi;
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            Guid ma = Guid.Parse(id);
            GHI_DANH ghidanh = db.GHI_DANH.Find(ma);
            db.GHI_DANH.Remove(ghidanh);
            db.SaveChanges();
            return RedirectToAction("Index", "THONG_KE", new { area = "Admin" });
        }
        public ActionResult home()
        {
            return View();
        }
    }
}