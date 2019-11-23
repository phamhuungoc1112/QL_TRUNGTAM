
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Abstracts;
using TrungTam.Areas.Admin.Models;
using Rotativa;
using System.Collections.Generic;
using System.Globalization;

namespace TrungTam.Areas.Admin.Controllers
{
    public class CONG_NOController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        public static List<ChiTietCongNo> quanque;
        // GET: Admin/CONG_NO
        //public static List<int> report = null;
        public void ReportsClass(string lop, DateTime dat)
        {
            if (!string.IsNullOrEmpty(lop))
            {
                var tt = (from a in db.CONG_NO
                          join b in db.CT_CONG_NO
                          on a.MA_CONG_NO equals b.MA_CONG_NO
                          where b.MA_LOP.ToString() == lop.ToString()
                          && a.TRANG_THAI == true
                         && a.NGAY_LAP_CONG_NO.Month == dat.Month
                         && a.NGAY_LAP_CONG_NO.Year == dat.Year
                          select b).ToList();
                ViewBag.DaThanhToan = tt.Count();
                ViewBag.TongTien = tt.Sum(n => n.CONG_NO.TONG_TIEN);
                //report.Add(ViewBag.DaThanhToan);
                //report.Add(ViewBag.TongTien);
                if (ViewBag.DaThanhToan == null)
                {
                    ViewBag.DaThanhToan = 0;
                    //report.Add(0);
                }
                if (ViewBag.TongTien == null)
                {
                    ViewBag.TongTien = 0;
                    //report.Add(0);
                }
            }
        }
        public ActionResult Index()
        {
            ViewBag.TongTien = 0;
            ViewBag.DaThanhToan = 0;
            ViewBag.Kiemtra = -1;
            ViewBag.kiemtraChuaHD = -1;
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            ViewBag.list_lop = new List<LOP_HOC>();
            var lop = db.LOP_HOC.Where(n => n.TRANG_THAI != -1).Select(n => n.MA_LOP).ToList();
            string lopLast = "";
            if (lop.Count() != 0 && lop != null)
            {
                lopLast = lop.Last().ToString();
                var tienhoc = (from a in db.CT_CONG_NO
                               where a.LOP_HOC.TRANG_THAI != -1 && a.LOP_HOC.MA_LOP.ToString() == lopLast.ToString()
                               && a.CONG_NO.NGAY_LAP_CONG_NO.Month == DateTime.Now.Month
                               && a.CONG_NO.NGAY_LAP_CONG_NO.Year == DateTime.Now.Year
                               select new HS_CongNo
                               {
                                   macongno = a.MA_CONG_NO.ToString(),
                                   tenlop = a.LOP_HOC.TEN_LOP,
                                   mahs = a.CONG_NO.MA_HS,
                                   hoten = a.CONG_NO.HOC_SINH.HO_TEN,
                                   ngaysinh = a.CONG_NO.HOC_SINH.NG_SINH.ToString(),
                                   gioitinh = a.CONG_NO.HOC_SINH.GIOI_TINH,
                                   sdt = a.CONG_NO.HOC_SINH.SDT,
                                   dongia = a.THANH_TIEN,
                                   trangthaihd = a.CONG_NO.TRANG_THAI
                               }).Distinct().ToList();
                ViewBag.list_lop = (from a in db.LOP_HOC
                                    where a.TRANG_THAI != -1
                                    select a).OrderBy(n => n.TEN_LOP).ToList();

                ViewBag.Kiemtra = (from a in db.CONG_NO
                                   join b in db.CT_CONG_NO
                                   on a.MA_CONG_NO equals b.MA_CONG_NO
                                   join c in db.LOP_HOC
                                   on b.MA_LOP equals c.MA_LOP
                                   where c.TRANG_THAI != -1
                                   && a.NGAY_LAP_CONG_NO.Month == DateTime.Now.Month
                                   select a).Count();
                ViewBag.kiemtraChuaHD = (from a in db.CONG_NO
                                         select a).Count();
                ReportsClass(lopLast, DateTime.Now);
                return View(tienhoc);
            }
            //IEnumerable<HS_CongNo> tienhoc = tienhoc1.Distinct();           
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index1(string lop)
        {
            string[] str = lop.Split('_');
            var mal = str[0];
            CultureInfo current = CultureInfo.CurrentCulture;
            DateTime dat = Convert.ToDateTime(str[1], System.Globalization.CultureInfo.GetCultureInfo(current.Name).DateTimeFormat);
            //var dat = DateTime.Parse(sa);
            //if (lop.Length > 36)
            //{
            //    dat = DateTime.Parse(lop.Substring(dodai));
            //}

            var n = (from a in db.CT_CONG_NO
                     where a.LOP_HOC.TRANG_THAI != -1 && a.LOP_HOC.MA_LOP.ToString() == mal
                     && a.CONG_NO.NGAY_LAP_CONG_NO.Month == DateTime.Now.Month
                     && a.CONG_NO.NGAY_LAP_CONG_NO.Year == DateTime.Now.Year
                     select new
                     {
                         macongno = a.MA_CONG_NO.ToString(),
                         tenlop = a.LOP_HOC.TEN_LOP,
                         mahs = a.CONG_NO.MA_HS,
                         hoten = a.CONG_NO.HOC_SINH.HO_TEN,
                         ngaysinh = a.CONG_NO.HOC_SINH.NG_SINH.ToString(),
                         gioitinh = a.CONG_NO.HOC_SINH.GIOI_TINH,
                         sdt = a.CONG_NO.HOC_SINH.SDT,
                         dongia = a.THANH_TIEN,
                         trangthaihd = a.CONG_NO.TRANG_THAI,
                         dadong = (from ctcn in db.CT_CONG_NO  ///tổng tiền cần đóng
                                   join cn in db.CONG_NO
                                   on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                   where
                                    ctcn.MA_LOP.ToString() == mal
                                   && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                   && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                   select ctcn).Sum(cl => cl.THANH_TIEN),

                         hsdong = (from ctcn in db.CT_CONG_NO  ///hocsinh đã đóng
                                   join cn in db.CONG_NO
                                   on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                   where
                                    ctcn.MA_LOP.ToString() == mal
                                   && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                   && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                   && cn.TRANG_THAI == true
                                   select ctcn).Count(),
                         tiendathu = (from ctcn in db.CT_CONG_NO  ///hocsinh đã đóng
                                      join cn in db.CONG_NO
                                      on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                      where
                                       ctcn.MA_LOP.ToString() == mal
                                      && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                      && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                      && cn.TRANG_THAI == true
                                      select ctcn).Sum(c => c.THANH_TIEN)
                     }).Distinct().ToList();

            //ReportsClass(mal, dat);
            return Json(n, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Filter1(string id)
        {
            //var dodai = Guid.NewGuid().ToString().Length;
            string[] str = id.Split('_');
            var mal = str[0];
            CultureInfo current = CultureInfo.CurrentCulture;
            DateTime dat = Convert.ToDateTime(str[1], System.Globalization.CultureInfo.GetCultureInfo(current.Name).DateTimeFormat);
            var fil = (from a in db.CT_CONG_NO
                       where a.LOP_HOC.TRANG_THAI != -1 && a.LOP_HOC.MA_LOP.ToString() == mal
                       && a.CONG_NO.NGAY_LAP_CONG_NO.Month == DateTime.Now.Month
                       && a.CONG_NO.NGAY_LAP_CONG_NO.Year == DateTime.Now.Year
                       select new
                       {
                           macongno = a.MA_CONG_NO.ToString(),
                           tenlop = a.LOP_HOC.TEN_LOP,
                           mahs = a.CONG_NO.MA_HS,
                           hoten = a.CONG_NO.HOC_SINH.HO_TEN,
                           ngaysinh = a.CONG_NO.HOC_SINH.NG_SINH.ToString(),
                           gioitinh = a.CONG_NO.HOC_SINH.GIOI_TINH,
                           sdt = a.CONG_NO.HOC_SINH.SDT,
                           dongia = a.THANH_TIEN,
                           trangthaihd = a.CONG_NO.TRANG_THAI,
                           //dadong = (from ctcn in db.CT_CONG_NO
                           //          join cn in db.CONG_NO
                           //          on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                           //          where
                           //           ctcn.MA_LOP.ToString() == mal
                           //          && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                           //          && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                           //          select ctcn).Sum(cl => cl.THANH_TIEN),
                           dadong = (from ctcn in db.CT_CONG_NO  ///tổng tiền cần đóng
                                     join cn in db.CONG_NO
                                     on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                     where
                                      ctcn.MA_LOP.ToString() == mal
                                     && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                     && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                     select ctcn).Sum(cl => cl.THANH_TIEN),

                           hsdong = (from ctcn in db.CT_CONG_NO  ///hocsinh đã đóng
                                     join cn in db.CONG_NO
                                     on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                     where
                                      ctcn.MA_LOP.ToString() == mal
                                     && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                     && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                     && cn.TRANG_THAI == true
                                     select ctcn).Count(),
                           tiendathu = (from ctcn in db.CT_CONG_NO  ///hocsinh đã đóng
                                        join cn in db.CONG_NO
                                        on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                        where
                                         ctcn.MA_LOP.ToString() == mal
                                        && cn.NGAY_LAP_CONG_NO.Month == dat.Month
                                        && cn.NGAY_LAP_CONG_NO.Year == dat.Year
                                        && cn.TRANG_THAI == true
                                        select ctcn).Sum(c => c.THANH_TIEN)
                       }).Distinct().ToList();
            //ReportsClass(mal, dat);
            return Json(fil, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCongNo()
        {
            var listHS = (from a in db.CT_LOP_HOC
                          join b in db.LOP_HOC
                          on a.MA_LOP equals b.MA_LOP
                          where b.TRANG_THAI != -1
                          select new
                          {
                              mahs = a.MA_HS
                          }).Distinct().ToList();

            var soluong = listHS.Count;
            var tienhoc = (from p in db.CT_LOP_HOC
                           from c in db.LOP_HOC
                           from b in db.HOC_SINH
                           from l in db.BANG_GIA_HOC_PHI
                           where p.MA_HS == b.MA_HS &&
                                     c.MA_LOP == p.MA_LOP &&
                                     b.MA_HS == p.MA_HS &&
                                     c.NGAY_AP_DUNG == l.NGAY_AP_DUNG &&
                                    //c.MA_LOP.ToString() == lon
                                    c.TRANG_THAI != -1
                           select new
                           {
                               mahs = b.MA_HS,
                               malop = c.MA_LOP,
                               dongia = l.DON_GIA
                           }).Distinct().ToList();
            var slHocSinh = tienhoc.Count;
            if (tienhoc != null && listHS != null && slHocSinh != 0 && soluong != 0)
            {
                for (int i = 0; i < soluong; i++)
                {
                    CONG_NO cONG_NO = new CONG_NO();
                    cONG_NO.MA_CONG_NO = Guid.NewGuid();
                    cONG_NO.NGAY_LAP_CONG_NO = DateTime.Now;
                    cONG_NO.MA_HS = listHS[i].mahs;
                    cONG_NO.TRANG_THAI = false;
                    db.CONG_NO.Add(cONG_NO);
                    for (int j = 0; j < slHocSinh; j++)
                    {
                        if (listHS[i].mahs == tienhoc[j].mahs)
                        {
                            CT_CONG_NO ct = new CT_CONG_NO();
                            ct.MA_CONG_NO = cONG_NO.MA_CONG_NO;
                            ct.MA_LOP = tienhoc[j].malop;
                            ct.THANH_TIEN = tienhoc[j].dongia;
                            db.CT_CONG_NO.Add(ct);
                        }
                    }
                    db.SaveChanges();
                }

            }
            //}
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Detail(string ab, string macn)
        {
            if (ab == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Details", "CONG_NO", new { id = ab, cn = macn });
        }
        //GET: Admin/CONG_NO/Details/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details()
        {
            try
            {
                string id1 = Request.QueryString["id"].ToString();
                string cn = Request.QueryString["cn"].ToString();
                //string id1 = ab;
                if (id1 == null || cn == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var CT_CongNo = (from a in db.CONG_NO
                                     //join b in db.HOC_SINH
                                     //on a.MA_HS equals b.MA_HS
                                     //join c in db.CT_LOP_HOC
                                     //on b.MA_HS equals c.MA_HS
                                     //join d in db.LOP_HOC
                                     //on c.MA_LOP equals d.MA_LOP
                                 join ctcn in db.CT_CONG_NO
                                 on a.MA_CONG_NO equals ctcn.MA_CONG_NO
                                 //join e in db.BANG_GIA_HOC_PHI
                                 //on d.NGAY_AP_DUNG equals e.NGAY_AP_DUNG
                                 where a.MA_HS == id1 &&
                                  ctcn.LOP_HOC.TRANG_THAI != -1 && a.MA_CONG_NO.ToString() == cn
                                 select new ChiTietCongNo
                                 {
                                     macongno = a.MA_CONG_NO.ToString(),
                                     malop = ctcn.MA_LOP,
                                     mahs = a.MA_HS,
                                     tenhs = ctcn.CONG_NO.HOC_SINH.HO_TEN,
                                     tenlop = ctcn.LOP_HOC.TEN_LOP,
                                     tenmon = ctcn.LOP_HOC.BANG_GIA_HOC_PHI.MON_HOC.TEN_MON,
                                     tenloai = ctcn.LOP_HOC.BANG_GIA_HOC_PHI.LOAI_LOP.TEN_LOAI,
                                     tenkhoi = ctcn.LOP_HOC.BANG_GIA_HOC_PHI.KHOI_LOP.TEN_KHOI,
                                     giatien = ctcn.THANH_TIEN
                                 }).Distinct().ToList();
                if (CT_CongNo == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CT_CongNo = CT_CongNo;
            }
            catch (Exception)
            {
            }
            ViewBag.KhuyenMai = (from a in db.KHUYEN_MAI
                                 select a).ToList();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PrintPayment(string id, string cn)
        {
            if (id == null || cn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CT_CongNo = (from a in db.CONG_NO.Include(n => n.KHUYEN_MAI)
                             join b in db.HOC_SINH
                             on a.MA_HS equals b.MA_HS
                             join c in db.CT_LOP_HOC
                             on b.MA_HS equals c.MA_HS
                             join d in db.LOP_HOC
                             on c.MA_LOP equals d.MA_LOP
                             join e in db.BANG_GIA_HOC_PHI
                             on d.NGAY_AP_DUNG equals e.NGAY_AP_DUNG
                             join m in db.MON_HOC
                             on e.MA_MON equals m.MA_MON
                             join l in db.LOAI_LOP
                             on e.MA_LOAI equals l.MA_LOAI
                             join f in db.KHOI_LOP
                             on e.MA_KHOI equals f.MA_KHOI
                             join ct in db.CT_CONG_NO
                             on a.MA_CONG_NO equals ct.MA_CONG_NO
                             where b.MA_HS == id &&
                                d.TRANG_THAI != -1 && a.MA_CONG_NO.ToString() == cn
                             select new ChiTietCongNo
                             {
                                 macongno = a.MA_CONG_NO.ToString(),
                                 malop = d.MA_LOP,
                                 mahs = b.MA_HS,
                                 tenhs = b.HO_TEN,
                                 tenlop = d.TEN_LOP,
                                 tenmon = m.TEN_MON,
                                 tenloai = l.TEN_LOAI,
                                 tenkhoi = f.TEN_KHOI,
                                 giatien = ct.THANH_TIEN,
                                 tiengiam = a.KHUYEN_MAI.TIEN_GIAM
                             }).Distinct().ToList();
            quanque = CT_CongNo;
            return RedirectToAction("Prints");
        }
        public ActionResult Prints()
        {
            return new ActionAsPdf("Export_PDF");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Export_PDF()
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
                return RedirectToAction("Index", "CONG_NO");
            }
            return RedirectToAction("Index", "CONG_NO");

        }

        //POST khuyen mai
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public JsonResult KhuyenMai(string km)
        {
            Guid dm = Guid.Parse(km);
            var khuyenmai1 = db.KHUYEN_MAI.Where(n => n.MA_KM == dm).Select(n => n.TIEN_GIAM).ToList();
            return Json(khuyenmai1, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/CONG_NO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public ActionResult Payment(string tong, string macn, string makm)
        {
            CONG_NO cONG_NO = db.CONG_NO.Find(Guid.Parse(macn));
            if (makm != "0")
                cONG_NO.MA_KM = Guid.Parse(makm);
            cONG_NO.NGAY_THANH_TOAN = DateTime.Now;
            cONG_NO.TONG_TIEN = decimal.Parse(tong);
            cONG_NO.TRANG_THAI = true;
            //db.CONG_NO.Add(cONG_NO);
            db.SaveChanges();
            //return new ActionAsPdf("Export_PDF");
            return Json(new { macn }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/CONG_NO/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONG_NO cONG_NO = db.CONG_NO.Find(id);
            if (cONG_NO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_HS = new SelectList(db.HOC_SINH, "MA_HS", "HO_TEN", cONG_NO.MA_HS);
            ViewBag.MA_KM = new SelectList(db.KHUYEN_MAI, "MA_KM", "TEN_KM", cONG_NO.MA_KM);
            return View(cONG_NO);
        }

        // POST: Admin/CONG_NO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_CONG_NO,TEN_CONG_NO,TONG_TIEN,NGAY_THANH_TOAN,MA_HS,MA_KM")] CONG_NO cONG_NO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONG_NO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_HS = new SelectList(db.HOC_SINH, "MA_HS", "HO_TEN", cONG_NO.MA_HS);
            ViewBag.MA_KM = new SelectList(db.KHUYEN_MAI, "MA_KM", "TEN_KM", cONG_NO.MA_KM);
            return View(cONG_NO);
        }

        // GET: Admin/CONG_NO/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONG_NO cONG_NO = db.CONG_NO.Find(id);
            if (cONG_NO == null)
            {
                return HttpNotFound();
            }
            return View(cONG_NO);
        }

        // POST: Admin/CONG_NO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CONG_NO cONG_NO = db.CONG_NO.Find(id);
            db.CONG_NO.Remove(cONG_NO);
            db.SaveChanges();
            return RedirectToAction("Index");
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
