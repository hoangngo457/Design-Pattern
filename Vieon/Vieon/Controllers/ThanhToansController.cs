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
    public class ThanhToansController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: ThanhToans
        public ActionResult Index()
        {
            var thanhToans = db.ThanhToans.Include(t => t.User).ToList();
            var NgayThucTe = DateTime.Now;
            foreach (var thanhToan in thanhToans)
            {
                if (NgayThucTe > thanhToan.NgayKetThuc)
                {
                    ChangeUserRole((int)thanhToan.ID_User);
                    db.ThanhToans.Remove(thanhToan);
                }
            }
            db.SaveChanges();
            return View(thanhToans);
        }
        private void ChangeUserRole(int userId)
        {
            User user = db.Users.Find(userId);

            if (user.RoleUser != "Admin")
            {
                user.RoleUser = "User";
                db.Entry(user).State = EntityState.Modified;
            }
        }

        // GET: ThanhToans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToan thanhToan = db.ThanhToans.Find(id);
            if (thanhToan == null)
            {
                return HttpNotFound();
            }
            return View(thanhToan);
        }

        // GET: ThanhToans/Create
        public ActionResult Create()
        {
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT");
            return View();
        }

        // POST: ThanhToans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ThanhToan,ID_User,NgayBatDau,NgayKetThuc")] ThanhToan thanhToan)
        {
            if (ModelState.IsValid)
            {
                db.ThanhToans.Add(thanhToan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", thanhToan.ID_User);
            return View(thanhToan);
        }

        // GET: ThanhToans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToan thanhToan = db.ThanhToans.Find(id);
            if (thanhToan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", thanhToan.ID_User);
            return View(thanhToan);
        }

        // POST: ThanhToans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ThanhToan,ID_User,NgayBatDau,NgayKetThuc")] ThanhToan thanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhToan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_User = new SelectList(db.Users, "ID_User", "SDT", thanhToan.ID_User);
            return View(thanhToan);
        }

        // GET: ThanhToans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToan thanhToan = db.ThanhToans.Find(id);
            if (thanhToan == null)
            {
                return HttpNotFound();
            }
            return View(thanhToan);
        }

        // POST: ThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThanhToan thanhToan = db.ThanhToans.Find(id);
            db.ThanhToans.Remove(thanhToan);
            db.SaveChanges();
            RefreshUserData();

            return RedirectToAction("Index");
        }
        private void RefreshUserData()
        {
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                ThanhToan thanhToan = db.ThanhToans.FirstOrDefault(t => t.ID_User == user.ID_User);

                if(user.RoleUser != "Admin")
                {
                    if (thanhToan == null)
                    {
                        user.RoleUser = "User";
                    }
                    else
                    {
                        user.RoleUser = "Vip";
                    }
                }
            }
            db.SaveChanges();
        }
        public ActionResult CancelSubscription(int userId)
        {
            ThanhToan thanhToan = db.ThanhToans.FirstOrDefault(t => t.ID_User == userId);
            db.ThanhToans.Remove(thanhToan);
            db.SaveChanges();
            RefreshUserData();
            RefreshUserData1();
            return RedirectToAction("Details", "UserKhachs", new { id = userId });
        }
        private void RefreshUserData1()
        {
            var idUser = Convert.ToInt32(Session["ID"]);
            var user = db.Users.FirstOrDefault(u => u.ID_User == idUser);

            if (user != null)
            {
                Session["Role"] = user.RoleUser;
            }
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
