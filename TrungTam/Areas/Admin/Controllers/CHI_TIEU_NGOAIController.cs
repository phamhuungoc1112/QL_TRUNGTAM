﻿using System;
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
    public class CHI_TIEU_NGOAIController : Controller
    {
        private QL_TRUNGTAMEntities db = new QL_TRUNGTAMEntities();

        // GET: Admin/CHI_TIEU_NGOAI
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            if (Session["ID"] == null)
                return Redirect("/Home/Index");
            var id = Session["ID"].ToString();
            if (id.First() != '9')
            {
                return Redirect("/Home/Index");
            }
            var chitieu = db.CHI_TIEU_NGOAI;
            return View(chitieu.ToList().OrderByDescending(p => p.NGAY).ToPagedList(page, pageSize));
        }

        // GET: Admin/CHI_TIEU_NGOAI/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHI_TIEU_NGOAI cHI_TIEU_NGOAI = db.CHI_TIEU_NGOAI.Find(id);
            if (cHI_TIEU_NGOAI == null)
            {
                return HttpNotFound();
            }
            return View(cHI_TIEU_NGOAI);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                CHI_TIEU_NGOAI ct = new CHI_TIEU_NGOAI();
                ct.MA_CT = Guid.NewGuid();
                ct.NGAY = DateTime.Parse(f["ngay"]);
                ct.TEN_CT = f["name"];
                ct.THANH_TIEN = double.Parse(f["thanhtien"]);
                db.CHI_TIEU_NGOAI.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Index", "CHI_TIEU_NGOAI", new { area = "Admin" });
            }
            return View();
        }
                // GET: Admin/CHI_TIEU_NGOAI/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHI_TIEU_NGOAI cHI_TIEU_NGOAI = db.CHI_TIEU_NGOAI.Find(id);
            if (cHI_TIEU_NGOAI == null)
            {
                return HttpNotFound();
            }
            return View(cHI_TIEU_NGOAI);
        }

        // POST: Admin/CHI_TIEU_NGOAI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_CT,TEN_CT,NGAY,THANH_TIEN")] CHI_TIEU_NGOAI cHI_TIEU_NGOAI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHI_TIEU_NGOAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHI_TIEU_NGOAI);
        }

        // GET: Admin/CHI_TIEU_NGOAI/Delete/5
       [HttpPost]
        public ActionResult Delete(Guid id)
        {
            CHI_TIEU_NGOAI cHI_TIEU_NGOAI = db.CHI_TIEU_NGOAI.Find(id);
            db.CHI_TIEU_NGOAI.Remove(cHI_TIEU_NGOAI);
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
