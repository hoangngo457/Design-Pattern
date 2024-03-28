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
    public class Phim_DaoDienController : Controller
    {
        private VieONEntities db = new VieONEntities();

        // GET: Phim_DaoDien
        public ActionResult Index()
        {
            var phim_DaoDien = db.Phim_DaoDien.Include(p => p.DaoDien).Include(p => p.Phim);
            return View(phim_DaoDien.ToList());
        }

        // GET: Phim_DaoDien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_DaoDien phim_DaoDien = db.Phim_DaoDien.Find(id);
            if (phim_DaoDien == null)
            {
                return HttpNotFound();
            }
            return View(phim_DaoDien);
        }

        // GET: Phim_DaoDien/Create
        public ActionResult Create()
        {
            ViewBag.ID_DaoDien = new SelectList(db.DaoDiens, "ID_DaoDien", "TenDaoDien");
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            return View();
        }

        // POST: Phim_DaoDien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Phim_DaoDien,ID_Phim,ID_DaoDien,VaiTro")] Phim_DaoDien phim_DaoDien)
        {
            if (ModelState.IsValid)
            {
                db.Phim_DaoDien.Add(phim_DaoDien);
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = phim_DaoDien.ID_Phim });
            }

            ViewBag.ID_DaoDien = new SelectList(db.DaoDiens, "ID_DaoDien", "TenDaoDien", phim_DaoDien.ID_DaoDien);
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_DaoDien.ID_Phim);
            return View(phim_DaoDien);
        }

        // GET: Phim_DaoDien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_DaoDien phim_DaoDien = db.Phim_DaoDien.Find(id);
            if (phim_DaoDien == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DaoDien = new SelectList(db.DaoDiens, "ID_DaoDien", "TenDaoDien", phim_DaoDien.ID_DaoDien);
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_DaoDien.ID_Phim);
            return View(phim_DaoDien);
        }

        // POST: Phim_DaoDien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Phim_DaoDien,ID_Phim,ID_DaoDien,VaiTro")] Phim_DaoDien phim_DaoDien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phim_DaoDien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = phim_DaoDien.ID_Phim });
            }
            ViewBag.ID_DaoDien = new SelectList(db.DaoDiens, "ID_DaoDien", "TenDaoDien", phim_DaoDien.ID_DaoDien);
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_DaoDien.ID_Phim);
            return View(phim_DaoDien);
        }

        // GET: Phim_DaoDien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_DaoDien phim_DaoDien = db.Phim_DaoDien.Find(id);
            if (phim_DaoDien == null)
            {
                return HttpNotFound();
            }
            return View(phim_DaoDien);
        }

        // POST: Phim_DaoDien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim_DaoDien phim_DaoDien = db.Phim_DaoDien.Find(id);
            int? idPhim = phim_DaoDien.ID_Phim;
            db.Phim_DaoDien.Remove(phim_DaoDien);
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
