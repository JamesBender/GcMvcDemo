using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlDemo.Web.Models;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Controllers
{
    public class DVDController : Controller
    {
        private DVDModel _dvdModel;

        public DVDController()
        {
            _dvdModel = new DVDModel();
        }

        // GET: DVD
        public ActionResult Index()
        {
            var listOfDVD = _dvdModel.GetListOfAllDVD();
            return View(listOfDVD);
        }

        // GET: DVD/Details/5
        public ActionResult Details(int id)
        {
            var dvd = _dvdModel.GetDvdDetails(id);
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
            var dvd = _dvdModel.GetDvdDetails(id);
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
            var dvd = _dvdModel.GetDvdDetails(id);
            return View(dvd);
        }

        // POST: DVD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DVD dvd)
        {
            try
            {
                _dvdModel.Delete(dvd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
