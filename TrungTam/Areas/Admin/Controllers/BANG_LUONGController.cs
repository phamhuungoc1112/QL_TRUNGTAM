using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;
using PagedList;
namespace TrungTam.Areas.Admin.Controllers
{
    public class BANG_LUONGController : Controller
    {
        private QL_TRUNGTAM1Entities db = new QL_TRUNGTAM1Entities();

        // GET: Admin/BANG_LUONG
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9')
            {
                return Redirect("/Home/Index");
            }
            ViewBag.stt = 0;
            return View(db.BANG_LUONG.OrderByDescending(l => l.TEN_LOAI).ToPagedList(page, pageSize));
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection r)
        {
            if (ModelState.IsValid)
            {
                BANG_LUONG bl = new BANG_LUONG();
                bl.MA_LOAI_LUONG = Guid.NewGuid();
                bl.TEN_LOAI = r["tenloai"];
                bl.DON_GIA = int.Parse(r["dongia"]);
                var min = r["soluongmin"];
                var max = r["soluongmax"];
                if (min == null || max == null)
                {
                    bl.SO_LUONG_MIN = 0;
                    bl.SO_LUONG_MAX = 0;
                }
                else
                {
                    bl.SO_LUONG_MIN = int.Parse(min);
                    bl.SO_LUONG_MAX = int.Parse(max);
                }
                db.BANG_LUONG.Add(bl);
                db.SaveChanges();
                //return RedirectToRoute("BANG_LUONG");
                return RedirectToAction("Index", "BANG_LUONG", new { area = "Admin" });
            }
            return View();
        }
        // GET: Admin/BANG_LUONG/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANG_LUONG bANG_LUONG = db.BANG_LUONG.Find(id);
            if (bANG_LUONG == null)
            {
                return HttpNotFound();
            }
            return View(bANG_LUONG);
        }

        // POST: Admin/BANG_LUONG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_LOAI_LUONG,SO_LUONG_MIN,SO_LUONG_MAX,TEN_LOAI,DON_GIA")] BANG_LUONG bANG_LUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANG_LUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANG_LUONG);
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
