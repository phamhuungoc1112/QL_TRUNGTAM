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

namespace TrungTam.Areas.Admin.Controllers
{
    public class CONG_NOController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
        public static List<ChiTietCongNo> quanque;
        // GET: Admin/CONG_NO
        public void ReportsClass(string lop, DateTime dat)
        {
            var tt = (from a in db.CONG_NO
                      join b in db.CT_CONG_NO
                      on a.MA_CONG_NO equals b.MA_CONG_NO
                      where b.MA_LOP.ToString() == lop.ToString()
                      && a.TRANG_THAI == true
                     && (a.NGAY_LAP_CONG_NO).Value.Month == dat.Month
                     && (a.NGAY_LAP_CONG_NO).Value.Year == dat.Year
                      select b);
            ViewBag.DaThanhToan = tt.Count();
            ViewBag.TongTien = tt.Sum(n => n.THANH_TIEN);
        }
        public ActionResult Index()
        {
            var listhocsinh = (from a in db.HOC_SINH
                               select a).OrderByDescending(p => p.MA_HS).ToList();
            var lop = db.LOP_HOC.Where(n => n.TRANG_THAI == 1).Select(n => n.MA_LOP).ToList().Last();
            var tienhoc = (from a in db.CONG_NO
                           join b in db.HOC_SINH
                           on a.MA_HS equals b.MA_HS
                           join c in db.CT_LOP_HOC
                           on b.MA_HS equals c.MA_HS
                           join d in db.LOP_HOC
                           on c.MA_LOP equals d.MA_LOP
                           join e in db.BANG_GIA_HOC_PHI
                           on d.NGAY_AP_DUNG equals e.NGAY_AP_DUNG
                           where d.TRANG_THAI == 1 && d.MA_LOP.ToString() == lop.ToString()
                           && (a.NGAY_LAP_CONG_NO).Value.Month == DateTime.Now.Month
                           select new HS_CongNo
                           {
                               macongno = a.MA_CONG_NO.ToString(),
                               tenlop = d.TEN_LOP,
                               mahs = b.MA_HS,
                               hoten = b.HO_TEN,
                               ngaysinh = b.NG_SINH.ToString(),
                               gioitinh = b.GIOI_TINH,
                               sdt = b.SDT,
                               dongia = e.DON_GIA,
                               trangthaihd = a.TRANG_THAI
                           }).Distinct().ToList();
            //IEnumerable<HS_CongNo> tienhoc = tienhoc1.Distinct();
            ViewBag.list_lop = (from a in db.LOP_HOC
                                where a.TRANG_THAI == 1
                                select a).OrderBy(n => n.TEN_LOP).ToList();
            ViewBag.Kiemtra = (from a in db.CONG_NO
                               join b in db.CT_CONG_NO
                               on a.MA_CONG_NO equals b.MA_CONG_NO
                               join c in db.LOP_HOC
                               on b.MA_LOP equals c.MA_LOP
                               where c.TRANG_THAI == 1 && (a.NGAY_LAP_CONG_NO).Value.Month == DateTime.Now.Month
                               select a).Count();
            ViewBag.kiemtraChuaHD = (from a in db.CONG_NO
                                     select a).Count();
            ReportsClass(lop.ToString(), DateTime.Now);
            return View(tienhoc);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index1(string lop)
        {

            var dodai = Guid.NewGuid().ToString().Length;
            var mal = lop.Substring(0, dodai);
            DateTime dat = DateTime.Now;
            if (lop.Length > 36)
            {
                dat = DateTime.Parse(lop.Substring(dodai));
            }
            var n = (from a in db.CONG_NO
                     join b in db.HOC_SINH
                     on a.MA_HS equals b.MA_HS
                     join c in db.CT_LOP_HOC
                     on b.MA_HS equals c.MA_HS
                     join d in db.LOP_HOC
                     on c.MA_LOP equals d.MA_LOP
                     join e in db.BANG_GIA_HOC_PHI
                     on d.NGAY_AP_DUNG equals e.NGAY_AP_DUNG
                     where d.MA_LOP.ToString() == mal
                     && (a.NGAY_LAP_CONG_NO).Value.Month == dat.Month
                     && (a.NGAY_LAP_CONG_NO).Value.Year == dat.Year
                     select new 
                     {
                         macongno = a.MA_CONG_NO.ToString(),
                         tenlop = d.TEN_LOP,
                         mahs = b.MA_HS,
                         hoten = b.HO_TEN,
                         ngaysinh = b.NG_SINH.ToString(),
                         gioitinh = b.GIOI_TINH,
                         sdt = b.SDT,
                         dongia = e.DON_GIA,
                         trangthaihd = a.TRANG_THAI,
                         dadong = (from ctcn in db.CT_CONG_NO
                                   join cn in db.CONG_NO
                                   on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                   where
                                    ctcn.MA_LOP.ToString() == mal
                                   && (cn.NGAY_LAP_CONG_NO).Value.Month == dat.Month
                                   && (cn.NGAY_LAP_CONG_NO).Value.Year == dat.Year
                                   select ctcn).Sum(cl => cl.THANH_TIEN)
                     }).Distinct().ToList();
            ReportsClass(mal, dat);
            return Json(n, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Filter1(string id)
        {
            //var fil = from cn in db.CONG_NO
            //          join ctcn in db.CT_CONG_NO
            //          on cn.MA_CONG_NO equals ctcn.MA_CONG_NO
            //          join hs in db.HOC_SINH
            //          on 
            var dodai = Guid.NewGuid().ToString().Length;
            var mal = id.Substring(0, dodai);
            var dat = DateTime.Parse(id.Substring(dodai));
            var fil = (from a in db.CONG_NO.Include(n => n.CT_CONG_NO)
                       join b in db.HOC_SINH
                       on a.MA_HS equals b.MA_HS
                       join c in db.CT_LOP_HOC
                       on b.MA_HS equals c.MA_HS
                       join d in db.LOP_HOC
                       on c.MA_LOP equals d.MA_LOP
                       join e in db.BANG_GIA_HOC_PHI
                       on d.NGAY_AP_DUNG equals e.NGAY_AP_DUNG
                       where d.MA_LOP.ToString() == mal
                        && (a.NGAY_LAP_CONG_NO).Value.Month == dat.Month
                        && (a.NGAY_LAP_CONG_NO).Value.Year == dat.Year
                       select new
                       {
                           macongno = a.MA_CONG_NO.ToString(),
                           tenlop = d.TEN_LOP,
                           mahs = b.MA_HS,
                           hoten = b.HO_TEN,
                           ngaysinh = b.NG_SINH.ToString(),
                           gioitinh = b.GIOI_TINH,
                           sdt = b.SDT,
                           dongia = e.DON_GIA,
                           trangthaihd = a.TRANG_THAI,
                           dadong = (from ctcn in db.CT_CONG_NO
                                     join cn in db.CONG_NO
                                     on ctcn.MA_CONG_NO equals cn.MA_CONG_NO
                                     where
                                      ctcn.MA_LOP.ToString() == mal
                                     && (cn.NGAY_LAP_CONG_NO).Value.Month == dat.Month
                                     && (cn.NGAY_LAP_CONG_NO).Value.Year == dat.Year
                                     select ctcn).Sum(n => n.THANH_TIEN)
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
                          where b.TRANG_THAI == 1
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
                                    c.TRANG_THAI == 1
                           select new
                           {
                               mahs = b.MA_HS,
                               malop = c.MA_LOP,
                               dongia = l.DON_GIA
                           }).Distinct().ToList();
            var slHocSinh = tienhoc.Count;
            if (tienhoc != null && listHS != null)
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
                                 where b.MA_HS == id1 &&
                                    d.TRANG_THAI == 1 && a.MA_CONG_NO.ToString() == cn
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
                                     giatien = e.DON_GIA
                                 }).ToList();
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
                             where b.MA_HS == id &&
                                d.TRANG_THAI == 1 && a.MA_CONG_NO.ToString() == cn
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
                                 giatien = e.DON_GIA,
                                 tiengiam = a.KHUYEN_MAI.TIEN_GIAM
                             }).ToList();
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
        [ValidateAntiForgeryToken]
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

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Payments()
        //{
        //    string a = "";
        //    try
        //    {
        //        a = Request.QueryString["id"].ToString();                
        //    }
        //    catch (Exception)
        //    {
        //        return HttpNotFound();
        //    }
        //    return new ActionAsPdf("Details", new { id = a }, new { });
        //}        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                //CONG_NO cONG_NO = new CONG_NO();
                //cONG_NO.MA_CONG_NO = Guid.NewGuid();
                //cONG_NO.TEN_CONG_NO = f["tencongno"];
                //cONG_NO.MA_HS = Request.QueryString["id"];
                //cONG_NO.MA_KM = Guid.Parse(f["khuyenmai"]);
                //cONG_NO.NGAY_THANH_TOAN = DateTime.Now;
                //cONG_NO.TONG_TIEN = decimal.Parse(f["tongtien"]);
                //var lay = f["lay"];
                //int dodai = (cONG_NO.MA_CONG_NO.ToString()).Length;
                //int i = 0;
                //while (i <= int.Parse(lay))
                //{
                //    CT_CONG_NO ct = new CT_CONG_NO();
                //    ct.MA_LOP = Guid.Parse(lay.Substring(0, dodai));
                //    ct.MA_CONG_NO = cONG_NO.MA_CONG_NO;
                //    string malop = ct.MA_LOP.ToString();
                //    ct.THANH_TIEN = decimal.Parse(f["malop"]);
                //    db.CT_CONG_NO.Add(ct);
                //    //db.SaveChanges();
                //}
                //db.CONG_NO.Add(cONG_NO);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MA_HS = new SelectList(db.HOC_SINH, "MA_HS", "HO_TEN", cONG_NO.MA_HS);
            //ViewBag.MA_KM = new SelectList(db.KHUYEN_MAI, "MA_KM", "TEN_KM", cONG_NO.MA_KM);
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
