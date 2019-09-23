using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrungTam.Areas.Admin.Abstracts;
using TrungTam.Areas.Admin.Models;
namespace TrungTam.Areas.Admin.Controllers
{
    public class LOP_HOCController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/LOP_HOC

        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var lOP_HOC = from l in db.LOP_HOC.Include(l => l.GIAO_VIEN).Include(n => n.BANG_GIA_HOC_PHI)
                          join k in db.KHOI_LOP
                          on l.BANG_GIA_HOC_PHI.MA_KHOI equals k.MA_KHOI
                          join ll in db.LOAI_LOP
                          on l.BANG_GIA_HOC_PHI.MA_LOAI equals ll.MA_LOAI
                          join m in db.MON_HOC
                          on l.BANG_GIA_HOC_PHI.MA_MON equals m.MA_MON
                          select new LopHoc_New
                          {
                              malop = l.MA_LOP,
                              tenlop = l.TEN_LOP,
                              siso = l.SI_SO,
                              hoten = l.GIAO_VIEN.HO_TEN,
                              tenkhoi = k.TEN_KHOI,
                              tenloai = ll.TEN_LOAI,
                              tenmon = m.TEN_MON,
                              trangthai = l.TRANG_THAI,
                              ngayketthuc = l.NGAY_KET_THUC,
                              ngaymolop = l.NGAY_MO_LOP,
                              ngayhoc = l.NGAY_BAT_DAU
                          };
            return View(lOP_HOC.OrderByDescending(l => l.tenlop).ToPagedList(page, pageSize));
        }
        //Chi ti?t h?c sinh trong l?p c?a View Details
        // GET: Admin/LOP_HOC/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            else
            {
                var hocsinh = (from a in db.LOP_HOC
                               join b in db.CT_LOP_HOC
                               on a.MA_LOP equals b.MA_LOP
                               join c in db.HOC_SINH
                               on b.MA_HS equals c.MA_HS
                               where a.MA_LOP == lOP_HOC.MA_LOP
                               select new Hoc_Sinh()
                               {
                                   mahs = c.MA_HS.ToString(),
                                   hoten = c.HO_TEN,
                                   ngaysinh = c.NG_SINH,
                                   gioitinh = c.GIOI_TINH,
                                   sdt = c.SDT,
                                   phuhuynh = c.PHU_HUYNH
                               }).OrderByDescending(l => l.hoten).ToList();

                if (hocsinh.Count() > 0)
                    ViewBag.list_hocsinh = hocsinh;
                else
                    ViewBag.list_hocsinh = null;
                var thoikhoabieu = (from tkb in db.THOI_KHOA_BIEU
                                    where tkb.MA_LOP == lOP_HOC.MA_LOP
                                    select new ThoiKhoaBieu
                                    {
                                        thu = tkb.THU,
                                        tgbt = tkb.THOI_GIAN_BD.ToString(),
                                        tgkt = tkb.THOI_GIAN_KT.ToString()
                                    }).ToList();
                ViewBag.listTKB = thoikhoabieu;
            }
            return View(lOP_HOC);
        }
        //GET: LOP_HOC/ListName
        //List g?i ý ? View Detail
        public JsonResult ListName(string q)
        {
            var data = (from a in db.HOC_SINH
                        where a.HO_TEN.Contains(q)
                        select a.HO_TEN).Distinct().ToList();
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Search(string search)
        {
            var malop = Guid.Parse(search.Substring(0, 36));
            var ten = search.Substring(36, search.Length - 36);
            var dem = (from a in db.HOC_SINH
                       join b in db.CT_LOP_HOC
                       on a.MA_HS equals b.MA_HS
                       join c in db.LOP_HOC
                       on b.MA_LOP equals c.MA_LOP
                       where a.HO_TEN == ten && c.MA_LOP == malop
                       select a).ToList();
            if (dem.Count() == 0)
            {
                var data = (from a in db.HOC_SINH
                            where a.HO_TEN.Contains(ten)
                            select new
                            {
                                mahs = a.MA_HS,
                                hoten = a.HO_TEN,
                                sdt = a.SDT
                            }).Distinct().OrderByDescending(l => l.hoten).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        //Thêm h?c sinh vào trong table chi ti?t
        //GET: LOP_HOC/Add     
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Add(string tong)
        {

            var mahs = tong.Substring(0, 10);
            var malop = tong.Substring(10, tong.Length - 10);
            //var ktra = (from lh in db.LOP_HOC
            //            where lh.TRANG_THAI == 1
            //            && lh.MA_LOP == Guid.Parse(malop)
            //            select lh).Count();
            //var tinhhp = from lop in db.LOP_HOC
            //             join hp in db.BANG_GIA_HOC_PHI
            //             on lop.NGAY_AP_DUNG equals hp.NGAY_AP_DUNG
            //             where lop.MA_LOP == Guid.Parse(malop)
            //             select hp;
            var statusClass = db.LOP_HOC.Find(Guid.Parse(malop)).TRANG_THAI;
            if (statusClass == 0)
            {
                CT_LOP_HOC ct_lop = new CT_LOP_HOC();
                ct_lop.MA_HS = mahs;
                ct_lop.MA_LOP = Guid.Parse(malop);
                db.CT_LOP_HOC.Add(ct_lop);
               
                CONG_NO cONG_NO = new CONG_NO();
                cONG_NO.MA_CONG_NO = Guid.NewGuid();
                cONG_NO.NGAY_LAP_CONG_NO = DateTime.Now;
                cONG_NO.MA_HS = mahs;
                cONG_NO.TRANG_THAI = false;
                CT_CONG_NO cT_CONG_NO = new CT_CONG_NO();
                cT_CONG_NO.MA_CONG_NO = cONG_NO.MA_CONG_NO;
                cT_CONG_NO.MA_LOP = Guid.Parse(malop);
                cT_CONG_NO.THANH_TIEN = ct_lop.LOP_HOC.BANG_GIA_HOC_PHI.DON_GIA;
                db.CONG_NO.Add(cONG_NO);
                db.CT_CONG_NO.Add(cT_CONG_NO);
                db.SaveChanges();
            }
            else if (statusClass == -1)
            {

            }
            else
            {
                CT_LOP_HOC ct_lop = new CT_LOP_HOC();
                ct_lop.MA_HS = mahs;
                ct_lop.MA_LOP = Guid.Parse(malop);
                db.CT_LOP_HOC.Add(ct_lop);
                db.SaveChanges();
            }

            return Json(tong, JsonRequestBehavior.AllowGet);
        }
        //POST: Admin/LOP_HOC/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.    
        //Truy?n d? li?u qua View Create
        public ActionResult Create()
        {
            var khoi = (from a in db.KHOI_LOP
                        join b in db.BANG_GIA_HOC_PHI
                        on a.MA_KHOI equals b.MA_KHOI
                        select new HocPhi_Khoi()
                        {
                            MA_KHOI = a.MA_KHOI,
                            TEN_KHOI = a.TEN_KHOI
                        }).Distinct().ToList();
            if (khoi.Count() > 0)
                ViewBag.listkhoi = khoi.ToList();
            else
                ViewBag.listkhoi = null;
            var banghp = (from a in db.BANG_GIA_HOC_PHI
                          join b in db.MON_HOC
                          on a.MA_MON equals b.MA_MON
                          join c in db.LOAI_LOP
                          on a.MA_LOAI equals c.MA_LOAI
                          join d in db.KHOI_LOP
                          on a.MA_KHOI equals d.MA_KHOI
                          select new BangHP_Khoi_Loai_Mon
                          {
                              NGAYAD = a.NGAY_AP_DUNG,
                              MA_KHOI = d.MA_KHOI,
                              TEN_KHOI = d.TEN_KHOI,
                              MA_MON = b.MA_MON,
                              TEN_MON = b.TEN_MON,
                              MA_LOAI = c.MA_LOAI,
                              TEN_LOAI = c.TEN_LOAI,
                              DON_GIA = a.DON_GIA,
                              SO_BUOI = a.SO_BUOI
                          }).ToList();
            //ViewBag.listmonhoc = monhoc.ToList();
            var listgiaovien = db.GIAO_VIEN.OrderBy(m => m.HO_TEN).ToList();
            if (listgiaovien.Count() > 0)
                ViewBag.listgiaovien = listgiaovien;
            else
                ViewBag.listgiaovien = listgiaovien;
            if (banghp.Count() > 0)
                ViewBag.listbanghp = banghp;
            else
                ViewBag.listbanghp = null;
            return View();
        }
        public class ajax_BangHP_Lop
        {
            public string a { get; set; }
            public string b { get; set; }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Create(ajax_BangHP_Lop l)
        {
            //var a = f.makhoi;
            JavaScriptSerializer js = new JavaScriptSerializer();
            var listTKB = js.Deserialize<List<ThoiKhoaBieu>>(l.b);
            listTKB.RemoveAt(0);
            var lop = js.Deserialize<Lop_Hoc>(l.a);
            //var banghp = (Lop_Hoc)js.Deserialize(Json, typeof(banghpJson));
            var ngayapdung = DateTime.Parse(lop.ngayad);
            var dmngayapdung = ngayapdung.ToString("yyyy/MM/dd HH:mm:ss");
            if (ModelState.IsValid)
            {
                LOP_HOC lh = new LOP_HOC();
                lh.MA_LOP = Guid.NewGuid();
                //lh.MA_KHOI = Guid.Parse(lop.makhoi);
                //lh.MA_MON = Guid.Parse(lop.mamon);
                //lh.MA_LOAI = Guid.Parse(lop.maloai);
                lh.TRANG_THAI = 1;
                lh.MA_GV = lop.magiaovien;
                lh.TEN_LOP = lop.tenlop;
                lh.NGAY_AP_DUNG = DateTime.Parse(dmngayapdung);

                lh.NGAY_MO_LOP = DateTime.Now;
                //lh.TRANG_THAI = 1;
                foreach (ThoiKhoaBieu i in listTKB)
                {
                    THOI_KHOA_BIEU tkb = new THOI_KHOA_BIEU();
                    tkb.MA_LOP = lh.MA_LOP;
                    tkb.THU = i.thu;
                    tkb.THOI_GIAN_BD = TimeSpan.Parse(i.tgbt);
                    tkb.THOI_GIAN_KT = TimeSpan.Parse(i.tgkt);
                    db.THOI_KHOA_BIEU.Add(tkb);
                }
                db.LOP_HOC.Add(lh);
                db.SaveChanges();
                return Json(l);
            }
            return Json(l);
        }
        //Ki?m tra th?i khóa bi?u có trùng không
        [HttpPost]
        [AllowAnonymous]
        public JsonResult KIEM_TRA_TKB(int thu)
        {
            //var tgbd_Them = TimeSpan.Parse(tkb.tgbt);
            //var tgkt_Them = TimeSpan.Parse(tkb.tgkt);
            //var khoangtg = tgkt - tgbd;          
            var a = (from lh in db.LOP_HOC
                     join tkb in db.THOI_KHOA_BIEU
                     on lh.MA_LOP equals tkb.MA_LOP
                     where tkb.THU == thu
                     select new
                     {
                         tenlop = lh.TEN_LOP,
                         //thoigianbd = (tkb.THOI_GIAN_BD).ToString(@"hh\:mm"),
                         thoigianbd = (tkb.THOI_GIAN_BD).ToString(),
                         thoigiankt = (tkb.THOI_GIAN_KT).ToString()
                     }).ToList();
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StatuClass(string id)
        {
            if (id == null || string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP = db.LOP_HOC.Find(Guid.Parse(id));
            if (lOP == null)
            {
                return HttpNotFound();
            }
            lOP.TRANG_THAI = 0;//CHUYỂN TRẠNG THÁI THÀNH ĐANG HỌC
            lOP.NGAY_BAT_DAU = DateTime.Now;
            db.SaveChanges();
            string x = "1";
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StatuTheEnd(string id)
        {
            if (id == null || string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP = db.LOP_HOC.Find(Guid.Parse(id));
            if (lOP == null)
            {
                return HttpNotFound();
            }
            lOP.TRANG_THAI = -1;//CHUYỂN TRẠNG THÁI THÀNH KẾT THÚC
            lOP.NGAY_KET_THUC = DateTime.Now;
            db.SaveChanges();
            string x = "1";
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/LOP_HOC/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", lOP_HOC.MA_GV);
            //ViewBag.MA_KHOI = new SelectList(db.KHOI_LOP, "MA_KHOI", "TEN_KHOI", lOP_HOC.MA_KHOI);
            //ViewBag.MA_LOAI = new SelectList(db.LOAI_LOP, "MA_LOAI", "TEN_LOAI", lOP_HOC.MA_LOAI);
            //ViewBag.MA_MON = new SelectList(db.MON_HOC, "MA_MON", "TEN_MON", lOP_HOC.MA_MON);
            return View(lOP_HOC);
        }

        // POST: Admin/LOP_HOC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_LOP,TEN_LOP,SI_SO,MA_LOAI,MA_MON,MA_KHOI,MA_GV")] LOP_HOC lOP_HOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOP_HOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", lOP_HOC.MA_GV);
            return View(lOP_HOC);
        }

        // GET: Admin/LOP_HOC/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            if (lOP_HOC == null)
            {
                return HttpNotFound();
            }
            return View(lOP_HOC);
        }

        // POST: Admin/LOP_HOC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LOP_HOC lOP_HOC = db.LOP_HOC.Find(id);
            db.LOP_HOC.Remove(lOP_HOC);
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
