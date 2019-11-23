using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Areas.Admin.Abstracts;
using PagedList;
using Rotativa;
namespace TrungTam.Areas.Admin.Controllers
{
    public class DANH_GIA_CUOI_THANGController : Controller
    {
        private static QL_TRUNGTAM1Entities db;
        public static IEnumerable<ReportCuoiThang> quanque;
        //public static int chuyencan, tongbuoi;
        public static QL_TRUNGTAM1Entities getDBInstance()
        {
            if (db == null)
            {
                db = new QL_TRUNGTAM1Entities();
            }
            return db;
        }
        // GET: Admin/DANH_GIA_CUOI_THANG
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '1')
            {
                return Redirect("/Home/Index");
            }
            var chonlop = getDBInstance().LOP_HOC.Where(p => p.MA_GV.Equals(id) && p.TRANG_THAI == 0).OrderBy(p => p.NGAY_BAT_DAU).ToList();
            ViewBag.chonlop = chonlop;
            return View();
        }
        public ActionResult Index1(string id, string ngay)
        {
            if (Session["ID"] == null)
                return Redirect("/Homde/Index");
            var id1 = Session["ID"].ToString();
            if (id1.First() != '1')
            {
                return Redirect("/Home/Index");
            }
            Guid malop = Guid.Parse(id);
            DateTime ngaylap = DateTime.Parse(ngay);
            var kiemtra = getDBInstance().DANH_GIA_CUOI_THANG.Where(p => p.NGAY_LAP.Value.Month == ngaylap.Month && p.NGAY_LAP.Value.Year.Equals(ngaylap.Year) && p.MA_LOP == malop).Count();
            return kiemtra == 0 ? Json((from ct in getDBInstance().CT_LOP_HOC
                                        where ct.MA_LOP == malop
                                        select new
                                        {
                                            mahs = ct.MA_HS,
                                            malop = id,
                                            hoten = ct.HOC_SINH.HO_TEN,
                                            dienthoai = ct.HOC_SINH.SDT,
                                            nhanxet = 0
                                        }).ToList(), JsonRequestBehavior.AllowGet) :
                                        Json((from dgct in getDBInstance().DANH_GIA_CUOI_THANG
                                              where dgct.MA_LOP == malop
                                              && dgct.NGAY_LAP.Value.Month == ngaylap.Month
                                              && dgct.NGAY_LAP.Value.Year == ngaylap.Year
                                              select new
                                              {
                                                  mahs = dgct.MA_HS,
                                                  malop = id,
                                                  hoten = dgct.HOC_SINH.HO_TEN,
                                                  dienthoai = dgct.HOC_SINH.SDT,
                                                  nhanxet = dgct.NHAN_XET
                                              }).ToList(), JsonRequestBehavior.AllowGet
                                             );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(FormCollection f)
        {
            var malop = Guid.Parse(f["lop"]);
            var ngaylap = DateTime.Parse(f["datepicker"]);
            var dem = getDBInstance().DANH_GIA_CUOI_THANG.Where(p => p.NGAY_LAP.Value.Month == ngaylap.Month && p.NGAY_LAP.Value.Year.Equals(ngaylap.Year) && p.MA_LOP == malop).Count();

            if (dem == 0)
            {
                var chonlop = getDBInstance().CT_LOP_HOC.Where(p => p.MA_LOP.Equals(malop)).ToList();
                foreach (var item in chonlop)
                {
                    DANH_GIA_CUOI_THANG dgct = new DANH_GIA_CUOI_THANG();
                    dgct.MA_DG = Guid.NewGuid();
                    dgct.NHAN_XET = f["nhanxet" + item.MA_HS];
                    dgct.NGAY_LAP = DateTime.Now;
                    dgct.MA_HS = f["mahs" + item.MA_HS];
                    dgct.MA_LOP = malop;
                    getDBInstance().DANH_GIA_CUOI_THANG.Add(dgct);
                }
                getDBInstance().SaveChanges();
                return RedirectToAction("Index", "DANH_GIA_CUOI_THANG");
            }
            var dg = getDBInstance().DANH_GIA_CUOI_THANG.Where(p => p.NGAY_LAP.Value.Month == ngaylap.Month && p.NGAY_LAP.Value.Year.Equals(ngaylap.Year) && p.MA_LOP == malop).ToList();
            foreach (var item in dg)
            {
                string a = "nhanxet" + item.MA_HS;
                var dgct = getDBInstance().DANH_GIA_CUOI_THANG.Find(item.MA_DG);
                var ca = Request.Form[a];
                dgct.NHAN_XET = f[a];
            }
            getDBInstance().SaveChanges();
            return RedirectToAction("Index", "DANH_GIA_CUOI_THANG");
        }
        public ActionResult IndexAdmin(int page = 1, int pageSize = 10)
        {
            var ds_hocvien = getDBInstance().HOC_SINH;
            return View(ds_hocvien.OrderByDescending(p => p.NG_VAO_HOC).ToPagedList(page, pageSize));
        }
        //public ActionResult PrintPayment(string)

        public ActionResult PrintReportMonth(string id, string date)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            System.Globalization.CultureInfo current = System.Globalization.CultureInfo.CurrentCulture;
            DateTime dat = Convert.ToDateTime(date, System.Globalization.CultureInfo.GetCultureInfo(current.Name).DateTimeFormat);
            var listmonho = getDBInstance().CT_BUOIHOC.Where(p =>
            p.BUOI_HOC.THOI_GIAN.Month.Equals(dat.Month) &&
            p.BUOI_HOC.LOP_HOC.TRANG_THAI.Value.Equals(0) &&
            p.BUOI_HOC.THOI_GIAN.Year.Equals(dat.Year) &&
            p.MA_HS.Equals(id)).Select(p => p).Distinct().ToList();


            var chuyencan = (from b in listmonho
                
                            group b by b.BUOI_HOC.MA_LOP into xa
                            select new 
                            {
                                malop = xa.Key.ToString(),
                                tong = xa.Count(),
                                chuyencan = xa.Where(p=>p.DIEM_DANH_HS.Value.Equals(true)).Count(),
                                diemtb = xa.Average(p=>p.DIEM)
                            }).ToList();            
            //Array tongbuoi = (from b in listmonho
            //               group b by b.BUOI_HOC.MA_LOP into xa
            //               select new 
            //               {
            //                   malop = xa.Key.ToString(),
            //                   tong = xa.Count()
            //               }).ToArray();            
            var danhgia =
               (from dgct in getDBInstance().DANH_GIA_CUOI_THANG
                where dgct.MA_HS.Equals(id)
                && dgct.NGAY_LAP.Value.Month.Equals(dat.Month)
                && dgct.NGAY_LAP.Value.Year.Equals(dat.Year)
                select new ReportCuoiThang
                {
                    //chuyencan = 0,
                    //tongbuoi = 0,
                    malop = dgct.MA_LOP.ToString(),
                    nhanxet = dgct.NHAN_XET,
                    lop = dgct.LOP_HOC.TEN_LOP,
                    tengv = dgct.LOP_HOC.GIAO_VIEN.HO_TEN,
                    tenhs = dgct.HOC_SINH.HO_TEN,
                    tenmon = dgct.LOP_HOC.BANG_GIA_HOC_PHI.MON_HOC.TEN_MON
                }).ToList();         
            foreach(var i in danhgia)
            {
                foreach(var a in chuyencan)
                {
                    if(i.malop == a.malop)
                    {
                        i.chuyencan = a.chuyencan;
                        i.tongbuoi = a.tong;
                        i.diemtb = (float)(Math.Floor(double.Parse(a.diemtb.ToString()) * 100) / 100);
                    }
                }
            }
            if (danhgia.Count() != 0)
            {
                quanque = danhgia;

            }                
            else quanque = null;

            //return quanque != null ? RedirectToAction("Prints", "DANH_GIA_CUOI_THANG") : RedirectToAction("Index", "DANH_GIA_CUOI_THANG"); ;
            return RedirectToAction("Prints", "DANH_GIA_CUOI_THANG");
        }

        public ActionResult Prints()
        {
            return new ActionAsPdf("PrintAllReport", "DANH_GIA_CUOI_THANG");
        }

        public ActionResult PrintAllReport()
        {
            try
            {
                return View(quanque);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "DANH_GIA_CUOI_THANG");
            }
            //return RedirectToAction("Index", "DANH_GIA_CUOI_THANG");
        }
    }
}