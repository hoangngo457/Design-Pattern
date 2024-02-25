using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;
using Vieon.Models.ShareViewModel;

namespace Vieon.Controllers
{
    public class BinhLuansController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: BinhLuans
        public ActionResult Index()
        {
            var binhLuans = db.BinhLuans.Include(b => b.Phim).Include(b => b.User);
            return View(binhLuans.ToList());
        }

        // GET: BinhLuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: BinhLuans/Create
        public ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT");
            return View();
        }

        // POST: BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShareViewModel model)
        {
            if (Session["TaiKhoan"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            BinhLuan binhLuan = new BinhLuan();
            binhLuan.ID_Phim = model.ID_Phim;
            if (model.NoiDung == null)
            {
                return Content("<script>alert('Hãy nhập nội dung bình luận'); window.location = '/PhimKhachs/Details/" + binhLuan.ID_Phim + "';</script>");
            }
            binhLuan.NoiDung = model.NoiDung;

            binhLuan.ID_User = Convert.ToInt32(Session["ID"]);
            binhLuan.NgayDang = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("details", "PhimKhachs", new { id = binhLuan.ID_Phim });
            }

            return View();
        }

        // GET: BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", binhLuan.ID_Phim);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", binhLuan.ID_User);
            return View(binhLuan);
        }

        // POST: BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BinhLuan,ID_Phim,ID_User,NoiDung,NgayDang")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Phims", new { id = binhLuan.ID_Phim });
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", binhLuan.ID_Phim);
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", binhLuan.ID_User);
            return View(binhLuan);
        }

        // GET: BinhLuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            int? maPhim = binhLuan.ID_Phim;
            db.BinhLuans.Remove(binhLuan);
            db.SaveChanges();
            return RedirectToAction("Edit", "Phims", new { id = maPhim });
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
