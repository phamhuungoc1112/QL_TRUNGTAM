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
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();
    
        // GET: Admin/BANG_LUONG
        public ActionResult Index(int page = 1, int pageSize = 10)
        {            
            return View(db.BANG_LUONG.OrderByDescending(l => l.TEN_LOAI).ToPagedList(page, pageSize));
        }

        // GET: Admin/BANG_LUONG/Details/5
        public ActionResult Details(Guid? id)
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

        // GET: Admin/BANG_LUONG/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Admin/BANG_LUONG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MA_LOAI_LUONG,TEN_LOAI,DON_GIA")] BANG_LUONG bANG_LUONG)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bANG_LUONG.MA_LOAI_LUONG = Guid.NewGuid();
        //        db.BANG_LUONG.Add(bANG_LUONG);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(bANG_LUONG);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection r)
        {
            if (ModelState.IsValid)
            {
                BANG_LUONG bl = new BANG_LUONG();
                bl.MA_LOAI_LUONG = Guid.NewGuid();
                bl.TEN_LOAI = r["tenloai"];
                bl.DON_GIA = decimal.Parse(r["dongia"]);
                db.BANG_LUONG.Add(bl);
                db.SaveChanges();
                //return RedirectToRoute("BANG_LUONG");
                return RedirectToAction("Index", "BANG_LUONG", new { area = "Admin"});
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
        public ActionResult Edit([Bind(Include = "MA_LOAI_LUONG,TEN_LOAI,DON_GIA")] BANG_LUONG bANG_LUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANG_LUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANG_LUONG);
        }

        // GET: Admin/BANG_LUONG/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: Admin/BANG_LUONG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BANG_LUONG bANG_LUONG = db.BANG_LUONG.Find(id);
            db.BANG_LUONG.Remove(bANG_LUONG);
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
