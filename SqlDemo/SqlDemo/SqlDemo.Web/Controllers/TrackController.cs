using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDemo.Data.Entities;
using SqlDemo.Web.Models;

namespace SqlDemo.Web.Controllers
{
    public class TrackController : Controller
    {
        private CDModel _cdModel;

        public TrackController()
        {
            _cdModel = new CDModel();
        }

        // GET: Track
        public ActionResult Index(int id)
        {
            var cd = _cdModel.GetCdDetails(id);

            ViewBag.CdId = id;
            ViewBag.CdTitle = cd.Title;
            ViewBag.Artist = cd.Artist;
            return View(cd.Tracks);
        }

        // GET: Track/Details/5
        public ActionResult Details(int id)
        {
            var track = _cdModel.GetTrackDetails(id);
            return View(track);
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
