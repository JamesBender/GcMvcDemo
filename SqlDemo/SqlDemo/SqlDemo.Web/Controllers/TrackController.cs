using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using SqlDemo.Data.Entities;
using SqlDemo.Web.Models;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Controllers
{
    public class TrackController : Controller
    {
        private readonly CDModel _cdModel;

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
        public ActionResult Create(int? id)
        {
            if (id == null) return View();
            var track = new Track {CD = new CD {Id = (int) id}};
            return View(track);
        }

        // POST: Track/Create
        [HttpPost]
        public ActionResult Create(Track track)
        {
            try
            {
                _cdModel.AddTrackToCD(track.CD.Id, track);

                return RedirectToAction("Index", new { id = track.CD.Id });
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Track/Edit/5
        public ActionResult Edit(int id)
        {
            var track = _cdModel.GetTrackDetails(id);
            return View(track);
        }

        // POST: Track/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Track track)
        {
            try
            {
                _cdModel.Save(track);

                return RedirectToAction("Index", new { id = track.CD.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Track/Delete/5
        public ActionResult Delete(int id)
        {
            var track = _cdModel.GetTrackDetails(id);
            return View(track);
        }

        // POST: Track/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Track track)
        {
            try
            {
                _cdModel.DeleteTrack(id);

                return RedirectToAction("Index", new { id = track.CD.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}
