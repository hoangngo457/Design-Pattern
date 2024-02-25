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
    public class DaoDiensController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: DaoDiens
        public ActionResult Index()
        {
            return View(db.DaoDiens.ToList());
        }

        // GET: DaoDiens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // GET: DaoDiens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DaoDiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DaoDien,TenDaoDien")] DaoDien daoDien)
        {
            if (ModelState.IsValid)
            {
                db.DaoDiens.Add(daoDien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daoDien);
        }

        // GET: DaoDiens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // POST: DaoDiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DaoDien,TenDaoDien")] DaoDien daoDien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daoDien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daoDien);
        }

        // GET: DaoDiens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // POST: DaoDiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DaoDien daoDien = db.DaoDiens.Find(id);
            db.DaoDiens.Remove(daoDien);
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
