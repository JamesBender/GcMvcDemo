using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FnhDemo.Web.Models;

namespace FnhDemo.Web.Controllers
{
    public class CDController : Controller
    {
        private CDModel _cdModel;

        public CDController()
        {
            AutomapperConfiguration.Configure();
            _cdModel = new CDModel();
        }

        // GET: CD
        public ActionResult Index()
        {
            var listOfCDs = _cdModel.GetAllCD();
            return View(listOfCDs);
        }

        // GET: CD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CD/Create
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

        // GET: CD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CD/Edit/5
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

        // GET: CD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CD/Delete/5
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
