using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using TrungTam.Function_Base;
using TrungTam.Areas.Admin.Abstracts;
namespace TrungTam.Areas.Admin.Controllers
{
    public class BUOI_HOCController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        private BASE bd = new BASE();
        // GET: Admin/BUOI_HOC
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var bUOI_HOC = db.BUOI_HOC.Include(b => b.BANG_LUONG).Include(b => b.LOP_HOC);
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
            ViewBag.chonlop = chonlop.ToList();
            return View(bUOI_HOC.ToList());
        }
        // GET: Admin/BUOI_HOC/Details/5
        public ActionResult Details()
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            ViewBag.THU = bd.DayOfWeek();
            var chonlop = db.LOP_HOC.OrderBy(p => p.NGAY_AP_DUNG);
            var buoihoc = db.BUOI_HOC.Include(p => p.LOP_HOC);
            ViewBag.buoihoc = buoihoc.ToList();
            ViewBag.chonlop = chonlop.ToList();
            return View();
        }
        // GET: Admin/BUOI_HOC/GV
        public ActionResult GV()
        {
            if(Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '1')
            {
                return Redirect("/Home/Index");
            }
            var chonlop = db.LOP_HOC.Where(p => p.MA_GV.Equals(id) && p.TRANG_THAI == 0).OrderBy(p => p.NGAY_AP_DUNG).ToList();
            ViewBag.chonlop = chonlop;
            return View();
        }
        //===================================================
        
        //===================================================
        [HttpGet]
        public ActionResult LoadBuoi(string id = "")
        {
            Guid malop = Guid.Parse(id);
            var buoihoc = db.BUOI_HOC.Include(q => q.GIAO_VIEN)
                .Select(p => new
                {
                    sttbuoi = p.STT_BUOI,
                    mabuoi = p.MA_BUOI,
                    thoigian = p.THOI_GIAN.ToString(),
                    magv = p.MA_GV,
                    hoten = p.GIAO_VIEN.HO_TEN,
                    sdt = p.GIAO_VIEN.SDT,
                    maluong = p.MA_LUONG,
                    malop = p.MA_LOP
                }).OrderByDescending(p => p.sttbuoi);
            return Json(buoihoc, JsonRequestBehavior.AllowGet);
        }
        //===================================================
        [HttpGet]
        public ActionResult LoadBuoi_gv(string id = "")
        {
            Guid malop = Guid.Parse(id);
            var buoihoc = db.BUOI_HOC.Where(p => p.TINH_TRANG == false).Include(q => q.GIAO_VIEN)
                .Select(p => new
                {
                    sttbuoi = p.STT_BUOI,
                    mabuoi = p.MA_BUOI,
                    thoigian = p.THOI_GIAN.ToString(),
                    magv = p.MA_GV,
                    hoten = p.GIAO_VIEN.HO_TEN,
                    sdt = p.GIAO_VIEN.SDT,
                    maluong = p.MA_LUONG,
                    malop = p.MA_LOP
                }).OrderByDescending(p => p.sttbuoi);
            return Json(buoihoc, JsonRequestBehavior.AllowGet);
        }
        //===================================================
        [HttpGet]
        public ActionResult Load_CTBuoi(string id = "")
        {
            Guid buoi = Guid.Parse(id);
            var ct_buoihoc = db.CT_BUOIHOC.Where(p => p.MA_BUOI.Equals(buoi)).Include(q => q.HOC_SINH)
                .Select(p => new
                {
                    mahs = p.MA_HS,
                    hoten = p.HOC_SINH.HO_TEN,
                    dienthoai = p.HOC_SINH.SDT,
                    diemdanhhs = p.DIEM_DANH_HS,
                    nhanxetgv = p.NHAN_XET_GV,
                    btvn = p.BAI_TAP_VN,
                    lido = p.LI_DO_VANG
                });
            return Json(ct_buoihoc, JsonRequestBehavior.AllowGet);
        }
        //======================== Json load chi tiết của lớp
        [HttpGet]
        public ActionResult LoadLop(string id = "")
        {
            Guid malp = Guid.Parse(id);
            var model = db.LOP_HOC.Where(p => p.MA_LOP.Equals(malp)).Include(a => a.GIAO_VIEN)
                                   .Select(p => new
                                   {
                                       hoten = p.GIAO_VIEN.HO_TEN,
                                       dienthoai = p.GIAO_VIEN.SDT,
                                       magv = p.MA_GV,
                                       malop = p.MA_LOP,
                                       tenlop = p.TEN_LOP,
                                       siso = p.SI_SO,
                                       tinhtrang = p.GIAO_VIEN.TRANG_THAI,
                                   });
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //=====================================================
        [HttpGet]
        public ActionResult LoadHS(string id = "")
        {
            Guid malp = Guid.Parse(id);
            var dsHS = db.CT_LOP_HOC.Where(p => p.MA_LOP.Equals(malp)).Include(b => b.HOC_SINH)
                                    .Select(p => new
                                    {
                                        MA_HS = p.MA_HS,
                                        HO_TEN = p.HOC_SINH.HO_TEN,
                                        SDT = p.HOC_SINH.SDT,
                                        DIA_CHI = p.HOC_SINH.DIA_CHI,
                                        TRANG_THAI = p.HOC_SINH.TINH_TRANG
                                    });
            return Json(dsHS, JsonRequestBehavior.AllowGet);
        }
        //=====================================================
        [HttpGet]
        public ActionResult LoadGV(string id = "")
        {
            var dsgv = db.GIAO_VIEN.Where(p => p.MA_GV != id && p.TRANG_THAI == true)
                                    .Select(p => new
                                    {
                                        MA_GV = p.MA_GV,
                                        HO_TEN = p.HO_TEN,
                                    });
            return Json(dsgv, JsonRequestBehavior.AllowGet);
        }
        //=====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                BUOI_HOC bh = new BUOI_HOC();
                bh.MA_BUOI = Guid.NewGuid();
                bh.MA_LOP = Guid.Parse(f["lopp"]);
                bh.TINH_TRANG = false;
                Guid malop = Guid.Parse(f["lopp"]);
                var luong = db.BANG_LUONG.OrderByDescending(p => p.SO_LUONG_MAX).ToList();
                //============ Thay đổi ======================
                var soluongmax = db.BANG_LUONG.Max(p => p.SO_LUONG_MAX);
                bh.THOI_GIAN = DateTime.Parse(f["ngay"]);
                Guid ma = new Guid();
                bool temp = false;
                //tìm mã lương phù hợp.
                foreach (var item in luong)
                {
                    if (int.Parse(f["siso"]) == item.SO_LUONG_MIN || int.Parse(f["siso"]) == item.SO_LUONG_MAX)
                    {
                        ma = item.MA_LOAI_LUONG;
                        temp = true;
                        break;
                    }
                }
                if(!temp)
                {
                    ma = luong.First().MA_LOAI_LUONG;
                }
                //============== Kết thúc ========================
                bh.MA_LUONG = ma;
                if (f["diemdanhGV"] == "1")
                    bh.MA_GV = f["magv"];
                else
                {
                    bh.MA_GV = f["daythay"];
                }
                var dsHS = db.CT_LOP_HOC.Where(p => p.MA_LOP.Equals(malop)).ToList();
                // duyệt bảng HS để thêm record vào db.CT_BUOIHOC
                foreach (var item in dsHS)
                {
                    CT_BUOIHOC ctbh = new CT_BUOIHOC();
                    ctbh.MA_BUOI = bh.MA_BUOI;
                    ctbh.MA_HS = item.MA_HS;
                    var diemdanh = f["diemdanh" + item.MA_HS];
                    ctbh.LI_DO_VANG = f["lido" + item.MA_HS];
                    if (diemdanh.Equals("0"))
                    {
                        
                        ctbh.DIEM_DANH_HS = false;
                    }
                    else
                        ctbh.DIEM_DANH_HS = true;
                    db.CT_BUOIHOC.Add(ctbh);
                }
                db.BUOI_HOC.Add(bh);
                db.SaveChanges();
                return RedirectToAction("Index", "BUOI_HOC", new { area = "Admin" });
            }
            return View();
        }
        //======================================================
        public ActionResult nhanxetGV(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                Guid buoi = Guid.Parse(f["buoi"]);
                var ct_bh = db.CT_BUOIHOC.Where(p => p.MA_BUOI.Equals(buoi)).ToList();
                foreach (var item in ct_bh)
                {
                    if (item.MA_HS.Equals(f["mahs" + item.MA_HS]))
                    {
                        if (f["diemdanh" + item.MA_HS] == "1")
                        {
                            item.NHAN_XET_GV = f["nhanxet" + item.MA_HS];
                            item.BAI_TAP_VN = f["BTVN" + item.MA_HS];
                            item.DIEM = float.Parse(f["diem" + item.MA_HS]);
                        }
                    }
                }
                var bh = db.BUOI_HOC.Find(buoi);
                bh.TINH_TRANG = true;
                db.SaveChanges();
                return RedirectToAction("GV", "BUOI_HOC", new { area = "Admin" });
            }
            return View();
        }
        //=========================thay đổi đây nè=============================
        [HttpGet]
        public JsonResult AllLop(string idd = "")
        {
            int id = bd.DayOfWeekk(idd);
            var thoigian = DateTime.Parse(idd);
            var chonlop = db.THOI_KHOA_BIEU.Where(p => p.THU.Equals(id) && p.LOP_HOC.TRANG_THAI == 0).Include(q => q.LOP_HOC)
                .Select(x => new 
                {
                    malop = x.LOP_HOC.MA_LOP,
                    tenlop = x.LOP_HOC.TEN_LOP,
                    trangthai = x.LOP_HOC.TRANG_THAI
                });
            return Json(chonlop, JsonRequestBehavior.AllowGet);
        }
        //====================kết thúc thay đổi==================================
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