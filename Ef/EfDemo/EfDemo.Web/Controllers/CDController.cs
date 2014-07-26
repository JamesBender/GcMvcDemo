using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EfDemo.Data;
using EfDemo.Web.Models;

namespace EfDemo.Web.Controllers
{
    public class CDController : Controller
    {
        private CdModel _model;

        public CDController()
        {
            _model = new CdModel();
        }

        // GET: CD
        public ActionResult Index()
        {
            var listOfCd = _model.GetListOfAllCd();

            return View(listOfCd);
        }

        // GET: CD/Details/5
        public ActionResult Details(int id)
        {
            var cd = _model.GetCdDetails(id);
            return View(cd);
        }

        // GET: CD/Create
        public ActionResult Create()
        {
            return View(new Models.ViewModels.CD());
        }

        // POST: CD/Create
        [HttpPost]
        public ActionResult Create(Models.ViewModels.CD cd)
        {
            try
            {
                _model.Create(cd);

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
            var cd = _model.GetCdDetails(id);
            return View(cd);
        }

        // POST: CD/Edit/5
        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
        public ActionResult Edit(int id, Models.ViewModels.CD cd)
        {
            try
            {
                _model.Save(cd);

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
            var cd = _model.GetCdDetails(id);
            return View(cd);
        }

        // POST: CD/Delete/5
        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
        public ActionResult Delete(int id, Models.ViewModels.CD cd)
        {
            try
            {
                var result = _model.Delete(cd);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
