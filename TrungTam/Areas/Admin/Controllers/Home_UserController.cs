using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Abstracts;

namespace TrungTam.Areas.Admin.Controllers
{
    public class Home_UserController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        // GET: Home_User
        public ActionResult Thoikhoabieu()
        {
            if(Session["ID"] == null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult lol(string id)
        {
            string ma = Session["ID"].ToString();
            if (ma.First().Equals('2'))
            {
                var chonlop = db.LOP_HOC.Join(db.THOI_KHOA_BIEU, lp => lp.MA_LOP, tkb => tkb.MA_LOP,
                           (lp, tkb) => new THOI_KHOA_BIEU_LOP_HOC
                           {
                               MA_LOP = lp.MA_LOP,
                               TEN_LOP = lp.TEN_LOP,
                               SI_SO = lp.SI_SO,
                               MA_GV = lp.MA_GV,
                               THU = tkb.THU,
                               THOI_GIAN_BD = tkb.THOI_GIAN_BD.ToString(),
                               THOI_GIAN_KT = tkb.THOI_GIAN_KT.ToString()
                           });
                var thoikhoabieu = (db.CT_LOP_HOC.Where(p => p.MA_HS.Equals(ma)).Join(chonlop
                    , tkb => tkb.MA_LOP, lp => lp.MA_LOP, (tkb, lp) => new THOI_KHOA_BIEU_LOP_HOC
                    {
                        MA_LOP = lp.MA_LOP,

                        TEN_LOP = lp.TEN_LOP,
                        SI_SO = lp.SI_SO,
                        MA_GV = lp.MA_GV,
                        THU = lp.THU,
                        THOI_GIAN_BD = lp.THOI_GIAN_BD,
                        THOI_GIAN_KT = lp.THOI_GIAN_KT
                    }).OrderBy(p => p.THOI_GIAN_BD)).ToList();
                foreach (var item in thoikhoabieu)
                {
                    string[] str1 = item.THOI_GIAN_BD.Split('.');
                    item.THOI_GIAN_BD = str1[0].ToString();
                    string[] str2 = item.THOI_GIAN_KT.Split('.');
                    item.THOI_GIAN_KT = str2[0].ToString();
                }
                return Json(thoikhoabieu, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var thoikhoabieu = db.LOP_HOC.Where(p => p.MA_GV.Equals(ma)).Join(db.THOI_KHOA_BIEU, lp => lp.MA_LOP, tkb => tkb.MA_LOP,
                           (lp, tkb) => new THOI_KHOA_BIEU_LOP_HOC
                           {
                               MA_LOP = lp.MA_LOP,
                               TEN_LOP = lp.TEN_LOP,
                               SI_SO = lp.SI_SO,
                               MA_GV = lp.MA_GV,
                               THU = tkb.THU,
                               THOI_GIAN_BD = tkb.THOI_GIAN_BD.ToString(),
                               THOI_GIAN_KT = tkb.THOI_GIAN_KT.ToString()
                           }).ToList();
                foreach(var item in thoikhoabieu)
                {
                    string[] str1 = item.THOI_GIAN_BD.Split('.');
                    item.THOI_GIAN_BD = str1[0].ToString();
                    string[] str2 = item.THOI_GIAN_KT.Split('.');
                    item.THOI_GIAN_KT = str2[0].ToString();
                }
                return Json(thoikhoabieu, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            var id = Session["ID"];
            var user = db.TAI_KHOAN.Find(id);
            user.MAT_KHAU = f["pass"];
            db.SaveChanges();
            return RedirectToAction("Thoikhoabieu","Home_User", new { Area = "Admin"});
        }
    }
}