using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;

namespace TrungTam.Areas.Admin.Controllers
{
    public class THONG_KEController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        // GET: Admin/THONG_KE
        public ActionResult Index()
        {
            var thongke_luong = from p in db.HOA_DON
                             group p by p.NGAY_THANH_TOAN.Month into hdgroup
                             select new
                             {
                                thang = hdgroup.Key,
                                thanhtien = hdgroup.Sum(item => item.TONG_TIEN)
                             };
            ViewBag.thongke_luong = thongke_luong.ToList();
           
            return View();
        }
    }
}