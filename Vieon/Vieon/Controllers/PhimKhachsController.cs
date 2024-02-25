using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class PhimKhachsController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: PhimKhachs
        public ActionResult Index(string searchText)
        {
            var phims = db.Phims.Include(s => s.Phim_TheLoai);
            if (!string.IsNullOrEmpty(searchText))
            {
                phims = phims.Where(s => s.TenPhim.Contains(searchText));
            }
            return View(phims.ToList());
        }
        public ActionResult PhimTheoTheLoai(int id)
        {
            var dsPhimTheoTheLoai = db.Phim_TheLoai
                                        .Where(ptl => ptl.ID_TheLoai == id)
                                        .Select(ptl => ptl.Phim)
                                        .ToList();

            return View("Index", dsPhimTheoTheLoai);
        }

        // GET: PhimKhachs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // GET: PhimKhachs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhimKhachs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Phim,TenPhim,MoTa,NamRaMat,ThoiLuong,LoaiPhim,HinhMinhHoa")] Phim phim)
        {
            if (ModelState.IsValid)
            {
                db.Phims.Add(phim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phim);
        }

        // GET: PhimKhachs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: PhimKhachs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Phim,TenPhim,MoTa,NamRaMat,ThoiLuong,LoaiPhim,HinhMinhHoa")] Phim phim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phim);
        }

        // GET: PhimKhachs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: PhimKhachs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
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
