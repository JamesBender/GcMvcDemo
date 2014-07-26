using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EfDemo.Data;

namespace EfDemo.Web.Controllers
{
    public class DVDController : Controller
    {
        private MediaManagerEntities db = new MediaManagerEntities();

        // GET: DVD
        public ActionResult Index()
        {
            return View(db.DVDs.ToList());
        }

        // GET: DVD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dVD = db.DVDs.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            return View(dVD);
        }

        // GET: DVD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DVD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RunningTime,IsSpecialEdition,Synopsis,Title,Genre,Year")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                db.DVDs.Add(dVD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dVD);
        }

        // GET: DVD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dVD = db.DVDs.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            return View(dVD);
        }

        // POST: DVD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RunningTime,IsSpecialEdition,Synopsis,Title,Genre,Year")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dVD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dVD);
        }

        // GET: DVD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dVD = db.DVDs.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            return View(dVD);
        }

        // POST: DVD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DVD dVD = db.DVDs.Find(id);
            db.DVDs.Remove(dVD);
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
