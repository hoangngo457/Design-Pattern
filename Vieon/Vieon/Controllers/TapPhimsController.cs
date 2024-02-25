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
    public class TapPhimsController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: TapPhims
        public ActionResult Index()
        {
            var tapPhims = db.TapPhims.Include(t => t.Phim);
            return View(tapPhims.ToList());
        }

        // GET: TapPhims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            return View(tapPhim);
        }

        // GET: TapPhims/Create
        public ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            return View();
        }

        // POST: TapPhims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TapPhim,ID_Phim,SoTap,TenTap,MoTa,ThoiLuong,NgayRaMat,UrlPhim")] TapPhim tapPhim)
        {
            if (ModelState.IsValid)
            {
                db.TapPhims.Add(tapPhim);
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = tapPhim.ID_Phim });
            }

            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        // GET: TapPhims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        // POST: TapPhims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TapPhim,ID_Phim,SoTap,TenTap,MoTa,ThoiLuong,NgayRaMat,UrlPhim")] TapPhim tapPhim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tapPhim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = tapPhim.ID_Phim });
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", tapPhim.ID_Phim);
            return View(tapPhim);
        }

        // GET: TapPhims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TapPhim tapPhim = db.TapPhims.Find(id);
            if (tapPhim == null)
            {
                return HttpNotFound();
            }
            return View(tapPhim);
        }

        // POST: TapPhims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TapPhim tapPhim = db.TapPhims.Find(id);
            int? idPhim = tapPhim.ID_Phim;
            db.TapPhims.Remove(tapPhim);
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
