using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FnhDemo.Web.Models;

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

        // GET: DVD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DVD/Edit/5
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

        // GET: DVD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DVD/Delete/5
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
