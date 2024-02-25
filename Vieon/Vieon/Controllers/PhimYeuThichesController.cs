using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class PhimYeuThichesController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: PhimYeuThiches
        public ActionResult Index()
        {
            var phimYeuThiches = db.PhimYeuThiches.Include(p => p.Phim).Include(p => p.User);
            return View(phimYeuThiches.ToList());
        }

        // GET: PhimYeuThiches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.Find(id);
            if (phimYeuThich == null)
            {
                return HttpNotFound();
            }
            return View(phimYeuThich);
        }

        // GET: PhimYeuThiches/Create
        public ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT");
            return View();
        }

        // POST: PhimYeuThiches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PhimYeuThich,ID_User,ID_Phim")] PhimYeuThich phimYeuThich)
        {
            if (ModelState.IsValid)
            {
                db.PhimYeuThiches.Add(phimYeuThich);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phimYeuThich.ID_Phim);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", phimYeuThich.ID_User);
            return View(phimYeuThich);
        }

        // GET: PhimYeuThiches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.Find(id);
            if (phimYeuThich == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phimYeuThich.ID_Phim);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", phimYeuThich.ID_User);
            return View(phimYeuThich);
        }

        // POST: PhimYeuThiches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PhimYeuThich,ID_User,ID_Phim")] PhimYeuThich phimYeuThich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phimYeuThich).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phimYeuThich.ID_Phim);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", phimYeuThich.ID_User);
            return View(phimYeuThich);
        }

        // GET: PhimYeuThiches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.Find(id);
            if (phimYeuThich == null)
            {
                return HttpNotFound();
            }
            return View(phimYeuThich);
        }

        // POST: PhimYeuThiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhimYeuThich phimYeuThich = db.PhimYeuThiches.Find(id);
            db.PhimYeuThiches.Remove(phimYeuThich);
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
