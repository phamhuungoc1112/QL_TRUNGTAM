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
using PagedList;
namespace TrungTam.Areas.Admin.Controllers
{
    public class HOC_SINHController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        private BASE bASE = new BASE();
        // GET: Admin/HOC_SINH
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var hOC_SINH = db.HOC_SINH.Where(p => p.TINH_TRANG == true);
            return View(hOC_SINH.OrderByDescending(m=>m.MA_HS).ToPagedList(page,pageSize));
        }

        // GET: Admin/HOC_SINH/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            if (hOC_SINH == null)
            {
                return HttpNotFound();
            }
            return View(hOC_SINH);
        }

        // GET: Admin/HOC_SINH/Create
        public ActionResult Create()
        {
            return View();
        }
        //=========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                HOC_SINH hs = new HOC_SINH();
                var MA_HS = db.HOC_SINH.Find("2000000001");
                if (MA_HS == null)
                    hs.MA_HS = "2000000001";
                else
                {
                    int ma = int.Parse(db.HOC_SINH.Select(m => m.MA_HS).ToList().Last()) + 1;
                    hs.MA_HS = ma.ToString();
                }
                hs.HO_TEN = f["name"];
                hs.SDT = f["SDT"];
                hs.NG_SINH = Convert.ToDateTime(f["ngaysinh"]);
                hs.GIOI_TINH = f["Gioitinh"];
                hs.MON_DK = f["mondk"];
                hs.TRUONG = f["truong"];
                hs.PHU_HUYNH = f["phuhuynh"];
                hs.DIA_CHI = f["diachi"];
                hs.KHOI = int.Parse(f["khoi"]);
                hs.SDT_PH = f["sdt_ph"];
                hs.TINH_TRANG = true;
                db.HOC_SINH.Add(hs);
                bASE.create_TAI_KHOAN(hs.MA_HS,f["SDT"]);
                db.SaveChanges();
                return RedirectToAction("Index", "HOC_SINH", new { area = "Admin" });
            }
            return View();
        }
        //=========================================================
        // GET: Admin/HOC_SINH/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOC_SINH hOC_SINH = db.HOC_SINH.Find(id);
            if (hOC_SINH == null)
            {
                return HttpNotFound();
            }
            return View(hOC_SINH);
        }

        // POST: Admin/HOC_SINH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_HS,HO_TEN,NG_SINH,GIOI_TINH,TINH_TRANG,KHOI,TRUONG,MON_DK,SDT,DIA_CHI,PHU_HUYNH,SDT_PH")] HOC_SINH hOC_SINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOC_SINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_HS = new SelectList(db.TAI_KHOAN, "TEN", "MAT_KHAU", hOC_SINH.MA_HS);
            return View(hOC_SINH);
        }

        // GET: Admin/HOC_SINH/Delete/5
        //[HttpPost]
        public ActionResult Delete(string id)
        {
            HOC_SINH hs = db.HOC_SINH.Find(id);
            hs.TINH_TRANG = false;
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            db.TAI_KHOAN.Remove(tAI_KHOAN);
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
