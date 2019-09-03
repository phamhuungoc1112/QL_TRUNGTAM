using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Abstracts;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Common;

namespace TrungTam.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login (LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var result = db.TAI_KHOAN.Count(x => x.TEN == model.UserName && x.MAT_KHAU == model.Password);
                if(result > 0)
                {
                    var user = db.TAI_KHOAN.SingleOrDefault(x => x.TEN == model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.TEN;
                    userSession.UserID = user.ID;
                    Session.Add(CommonContants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
            }
            return View();
        }
    }
}