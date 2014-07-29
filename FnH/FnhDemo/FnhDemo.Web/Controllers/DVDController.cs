using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FnhDemo.Web.Models;
using FnhDemo.Web.Models.ViewModels;

namespace FnhDemo.Web.Controllers
{
    public class DVDController : Controller
    {
        private DvdModel _dvdModel;

        public DVDController()
        {
            AutomapperConfiguration.Configure();
            _dvdModel = new DvdModel();
        }

        // GET: DVD
        public ActionResult Index()
        {
            var listOfDvds = _dvdModel.GetAllDvd();
            return View(listOfDvds);
        }

        // GET: DVD/Details/5
        public ActionResult Details(int id)
        {
            var dvd = _dvdModel.GetDvd(id);
            return View(dvd);
        }

        // GET: DVD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DVD/Create
        [HttpPost]
        public ActionResult Create(DVD dvd)
        {
            try
            {
                _dvdModel.Save(dvd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DVD/Edit/5
        public ActionResult Edit(int id)
        {
            var dvd = _dvdModel.GetDvd(id);
            return View(dvd);
        }

        // POST: DVD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DVD dvd)
        {
            try
            {
                _dvdModel.Save(dvd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DVD/Delete/5
        public ActionResult Delete(int id)
        {
            var dvd = _dvdModel.GetDvd(id);
            return View(dvd);
        }

        // POST: DVD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DVD dvd)
        {
            try
            {
                _dvdModel.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
