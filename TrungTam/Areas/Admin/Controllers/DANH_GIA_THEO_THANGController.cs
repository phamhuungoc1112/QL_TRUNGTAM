using System;
using System.Linq;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Abstracts;
using Rotativa;
using System.Collections.Generic;
namespace TrungTam.Areas.Admin.Controllers
{
    public class DANH_GIA_THEO_THANGController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        public static IEnumerable<ReportDanhGia> quanque;
        // GET: Admin/DANH_GIA_THEO_THANG
        public ActionResult Index()
        {
            var lop = db.LOP_HOC;
            return View(lop);
        }
        [HttpGet]
        public ActionResult Index1(string id)
        {
            var ma = Guid.Parse(id);
            var ct_lop = from l in db.LOP_HOC
                         join ctl in db.CT_LOP_HOC
                         on l.MA_LOP equals ctl.MA_LOP
                         join hs in db.HOC_SINH
                         on ctl.MA_HS equals hs.MA_HS
                         where l.MA_LOP == ma
                         select new
                         {
                             mahs = hs.MA_HS,
                             tenhs = hs.HO_TEN,
                             ngaysinh = hs.NG_SINH.ToString(),
                         };
            return Json(ct_lop, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintReportMonth(string id, string date)
        {
            //string[] str = id.Split('_');
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            string mahs = id.Substring(0, 10);
            //string dateloc = id.Substring(10);
            var tg = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            var chitiet = (from b in db.BUOI_HOC
                           join p in db.CT_BUOIHOC
                           on b.MA_BUOI equals p.MA_BUOI
                           join g in db.GIAO_VIEN
                           on b.MA_GV equals g.MA_GV                         
                           where p.MA_HS.Equals(mahs) && p.BUOI_HOC.THOI_GIAN.Month.Equals(tg.Month) && p.BUOI_HOC.THOI_GIAN.Year.Equals(tg.Year)
                           select new ReportDanhGia
                           {
                               tenlop = p.BUOI_HOC.LOP_HOC.TEN_LOP,
                               buoihoc = p.BUOI_HOC.STT_BUOI,
                               tengv = g.HO_TEN,
                               tenmon = p.BUOI_HOC.LOP_HOC.BANG_GIA_HOC_PHI.MON_HOC.TEN_MON,
                               diemdanh = p.DIEM_DANH_HS,
                               btvn = p.BAI_TAP_VN,
                               nhanxet = p.NHAN_XET_GV,
                               tenhs = p.HOC_SINH.HO_TEN 
                           }).ToList();
            //var 
            quanque = chitiet;
            return RedirectToAction("Prints", "DANH_GIA_THEO_THANG");
        }

        public ActionResult Prints()
        {
            return new ActionAsPdf("PrintReport", "DANH_GIA_THEO_THANG");
        }

        public ActionResult PrintReport()
        {
            try
            {
                if (quanque != null)
                {
                    return View(quanque);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "DANH_GIA_THEO_THANG");
            }
            return RedirectToAction("Index", "DANH_GIA_THEO_THANG");
        }
    }
}