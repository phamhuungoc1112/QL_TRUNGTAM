using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Controllers;
using TrungTam.Areas.Admin.Models;
using TrungTam.Function_Base;
using TrungTam.Areas.Admin.Abstracts;
using TrungTam.Areas.Admin.Common;

namespace TrungTam.Controllers
{
    public class HomeController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        public ActionResult Index()
        {
            var noidung = db.TRANG_CHU.OrderByDescending(p => p.NGAY_AP_DUNG);
            ViewBag.noidung = noidung.ToList();
            var khuyenmai = db.KHUYEN_MAI.OrderBy(p => p.SO_MON_DK);
            ViewBag.khuyenmai = khuyenmai.ToList();
           //var khoi = db.KHOI_LOP.OrderBy(p => p.TEN_KHOI);
           // ViewBag.khoi = khoi.ToList();
            var hocphi = (from b in db.BANG_GIA_HOC_PHI                   
                          select b).ToList();                         
            return View(hocphi);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var password = /*Encryptor.MD5Hash(*/f["pass"]/*)*/;
                var name = f["name"];
                var result = db.TAI_KHOAN.Count(x => x.TEN == name  && x.MAT_KHAU == password);
                if (result > 0)
                {
                    var user = db.TAI_KHOAN.SingleOrDefault(x => x.TEN == name);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TEN;
                    userSession.UserID = user.ID;
                    Session.Add(CommonContants.USER_SESSION, userSession);
                    Session["ID"] = user.ID;
                    Session["Name"] = user.TEN;
                    Session["Pass"] = user.MAT_KHAU;
                    if (user.ID.StartsWith("9") || user.ID.StartsWith("8"))
                        return RedirectToAction("Index", "THONG_KE", new { area = "Admin" });
                    else if(user.ID.StartsWith("1"))
                    {
                        return RedirectToAction("Thoikhoabieu", "Home_User", new { area = "Admin" });
                    }
                    else
                        return RedirectToAction("Thoikhoabieu", "Home_User", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return Redirect("/Home/Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/Home/Index");
        }
        
        public ActionResult ghidanh(FormCollection f)
        {
            GHI_DANH gHI_DANH = new GHI_DANH();
            gHI_DANH.MA_TB = Guid.NewGuid();
            gHI_DANH.HO_TEN = f["txt_hoten"];
            gHI_DANH.NG_SINH = DateTime.Parse(f["txt_ngaysinh"]);
            gHI_DANH.SDT = f["txt_SDT"];
            gHI_DANH.DIA_CHI = f["txt_diachi"];
            gHI_DANH.TRUONG = f["txt_truong"];
            gHI_DANH.TINH_TRANG = false;
            gHI_DANH.NOI_DUNG = f["txt_noidung"];
            db.GHI_DANH.Add(gHI_DANH);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}