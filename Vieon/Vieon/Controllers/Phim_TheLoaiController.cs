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
    public class Phim_TheLoaiController : Controller
    {
        private VieONEntities db = new VieONEntities();

        // GET: Phim_TheLoai
        public ActionResult Index()
        {
            var phim_TheLoai = db.Phim_TheLoai.Include(p => p.Phim).Include(p => p.TheLoai);
            return View(phim_TheLoai.ToList());
        }

        // GET: Phim_TheLoai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(phim_TheLoai);
        }

        // GET: Phim_TheLoai/Create
        public ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai");
            return View();
        }

        // POST: Phim_TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Phim_TheLoai,ID_Phim,ID_TheLoai")] Phim_TheLoai phim_TheLoai)
        {
            if (ModelState.IsValid)
            {
                db.Phim_TheLoai.Add(phim_TheLoai);
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = phim_TheLoai.ID_Phim });
            }

            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        // GET: Phim_TheLoai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        // POST: Phim_TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Phim_TheLoai,ID_Phim,ID_TheLoai")] Phim_TheLoai phim_TheLoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phim_TheLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = phim_TheLoai.ID_Phim });
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        // GET: Phim_TheLoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(phim_TheLoai);
        }

        // POST: Phim_TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            int? idPhim = phim_TheLoai.ID_Phim;
            db.Phim_TheLoai.Remove(phim_TheLoai);
            db.SaveChanges();
            return RedirectToAction("Edit", "Phims", new { id = idPhim });
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
