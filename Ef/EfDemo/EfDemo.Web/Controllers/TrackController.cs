using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfDemo.Web.Models;
using EfDemo.Web.Models.ViewModels;

namespace EfDemo.Web.Controllers
{
    public class TrackController : Controller
    {
        private CdModel _cdModel;

        public TrackController()
        {
            _cdModel = new CdModel();
        }

        // GET: Track
        public ActionResult Index(int? cdId)
        {
            var cd = _cdModel.GetCdDetails(cdId);
            if (cd.Tracks == null)
            {
                cd.Tracks = new Collection<Track>();
            }
            ViewBag.CdId = cdId;
            return View(cd.Tracks);
        }

        // GET: Track/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Track/Create
        public ActionResult Create(int? id)
        {
            var track = new Track {CDId = id};
            return View(track);
        }

        // POST: Track/Create
        [HttpPost]
//        public ActionResult Create(FormCollection collection)
        public ActionResult Create(Track track)
        {
            try
            {
                var x = _cdModel.AddTrackToCd(track.CDId, track);

                return RedirectToAction("Index", new {track.CDId});
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
