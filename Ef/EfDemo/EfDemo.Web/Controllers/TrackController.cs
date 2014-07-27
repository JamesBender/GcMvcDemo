using System;
using System.Web.Mvc;
using EfDemo.Web.Models;
using EfDemo.Web.Models.ViewModels;

namespace EfDemo.Web.Controllers
{
    public class TrackController : Controller
    {
        private readonly CdModel _cdModel;

        public TrackController()
        {
            _cdModel = new CdModel();
        }

        // GET: Track
        public ActionResult Index(int? id)
        {
            var cd = _cdModel.GetCdDetails(id);

            ViewBag.CdId = id;
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
            var track = new Track {CDId = id};
            return View(track);
        }

        // POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Track track)
        {
            try
            {
                _cdModel.AddTrackToCd(track.CDId, track);

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
            var track = _cdModel.GetTrackDetails(id);
            return View(track);
        }

        // POST: Track/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Track track)
        {
            try
            {
                _cdModel.Save(track);

                return RedirectToAction("Index", new {id = track.CDId});
            }
            catch (Exception)
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Track track)
        {
            try
            {
                _cdModel.Delete(track);

                return RedirectToAction("Index", new {id = track.CDId});
            }
            catch
            {
                return View();
            }
        }
    }
}
