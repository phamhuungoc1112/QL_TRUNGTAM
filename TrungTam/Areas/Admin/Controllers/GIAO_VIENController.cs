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
using PagedList;
namespace TrungTam.Areas.Admin.Controllers
{
    public class GIAO_VIENController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();
        private BASE bASE = new BASE();
        // GET: Admin/GIAO_VIEN
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var gIAO_VIEN = db.GIAO_VIEN.Where(p => p.TRANG_THAI == true);
            return View(gIAO_VIEN.OrderBy(m => m.HO_TEN).ToPagedList(page, pageSize));
        }
        // GET: Admin/GIAO_VIEN/Create
        public ActionResult Create()
        {
           
            return View();
        }
        //====================thay chỗ này==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                GIAO_VIEN gv = new GIAO_VIEN();
                var ma_gv = db.GIAO_VIEN.Find("1000000001");
                if (ma_gv == null)
                    gv.MA_GV = "1000000001";
                else
                {
                    int ma = int.Parse(db.GIAO_VIEN.Select(m => m.MA_GV).ToList().Last()) + 1;
                    gv.MA_GV = ma.ToString();
                }
                gv.HO_TEN = f["name"];
                gv.SDT = f["SDT"];
                gv.NG_SINH = Convert.ToDateTime(f["ngaysinh"]);
                gv.GIOI_TINH = f["gioitinh"];
                gv.EMAIL = f["email"];
                gv.TRANG_THAI = true;
                db.GIAO_VIEN.Add(gv);
                bASE.create_TAI_KHOAN(gv.MA_GV,f["SDT"]);
                db.SaveChanges();
                return RedirectToAction("Index", "GIAO_VIEN", new { area = "Admin" });
            }
            return View();
        }
        //=======================Kết thúc đoạn thay===============================
        // GET: Admin/GIAO_VIEN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAO_VIEN gIAO_VIEN = db.GIAO_VIEN.Find(id);
            if (gIAO_VIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_GV = new SelectList(db.TAI_KHOAN, "TEN", "MAT_KHAU", gIAO_VIEN.MA_GV);
            return View(gIAO_VIEN);
        }
        //===============================================
        // POST: Admin/GIAO_VIEN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //====================thay chỗ này==================
        public ActionResult Edit([Bind(Include = "MA_GV,HO_TEN,SDT,GIOI_TINH,TRANG_THAI,EMAIL,NG_SINH")] GIAO_VIEN gIAO_VIEN)
        {
            //====================kết thúc chỗ thay==================
            if (ModelState.IsValid)
            {
                db.Entry(gIAO_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_GV = new SelectList(db.TAI_KHOAN, "TEN", "MAT_KHAU", gIAO_VIEN.MA_GV);
            return View(gIAO_VIEN);
        }
        // POST: Admin/GIAO_VIEN/Delete/5

        public ActionResult Delete(string id)
        {
            //====================thay chỗ này==================
            GIAO_VIEN gIAO_VIEN = db.GIAO_VIEN.Find(id);
            gIAO_VIEN.TRANG_THAI = false;
            //====================kết thúc chỗ thay==================
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
