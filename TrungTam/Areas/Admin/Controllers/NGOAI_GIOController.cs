﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrungTam.Areas.Admin.Models;

namespace TrungTam.Areas.Admin.Controllers
{
    public class NGOAI_GIOController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/NGOAI_GIO
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9' && id.First() != '8')
            {
                return Redirect("/Home/Index");
            }
            var nGOAI_GIO = db.NGOAI_GIO.Include(n => n.BANG_LUONG).Include(n => n.GIAO_VIEN);
            ViewBag.listgv = db.GIAO_VIEN.ToList();
            ViewBag.listhd = db.BANG_LUONG.ToList();
            return View(nGOAI_GIO.ToList());
        }

        // GET: Admin/NGOAI_GIO/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGOAI_GIO nGOAI_GIO = db.NGOAI_GIO.Find(id);
            if (nGOAI_GIO == null)
            {
                return HttpNotFound();
            }
            return View(nGOAI_GIO);
        }

      
        // GET: Admin/NGOAI_GIO/Edit/5
        //===================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                NGOAI_GIO ng = new NGOAI_GIO();
                ng.MA_NGOAI_GIO = Guid.NewGuid();
                ng.MA_LUONG = Guid.Parse(f["maluong"]);
                ng.MA_GV = f["magv"];
                ng.NGAY_LAM = DateTime.Parse(f["ngaylam"]);
                ng.SO_LUONG = int.Parse(f["soluong"]);
                ng.TINH_TRANG = false;
                db.NGOAI_GIO.Add(ng);
                db.SaveChanges();
                return RedirectToAction("Index", "NGOAI_GIO", new { area = "Admin" });
            }
            return View();
        }
        //===================================================
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGOAI_GIO nGOAI_GIO = db.NGOAI_GIO.Find(id);
            if (nGOAI_GIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_LUONG = new SelectList(db.BANG_LUONG, "MA_LOAI_LUONG", "TEN_LOAI", nGOAI_GIO.MA_LUONG);
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", nGOAI_GIO.MA_GV);
            return View(nGOAI_GIO);
        }

        // POST: Admin/NGOAI_GIO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_NGOAI_GIO,MA_LUONG,MA_GV,NGAY_LAM,SO_LUONG")] NGOAI_GIO nGOAI_GIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGOAI_GIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_LUONG = new SelectList(db.BANG_LUONG, "MA_LOAI_LUONG", "TEN_LOAI", nGOAI_GIO.MA_LUONG);
            ViewBag.MA_GV = new SelectList(db.GIAO_VIEN, "MA_GV", "HO_TEN", nGOAI_GIO.MA_GV);
            return View(nGOAI_GIO);
        }
        // POST: Admin/NGOAI_GIO/Delete/5
        [HttpPost]
        public ActionResult Delete (Guid id)
        {
            NGOAI_GIO nGOAI_GIO = db.NGOAI_GIO.Find(id);
            db.NGOAI_GIO.Remove(nGOAI_GIO);
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
