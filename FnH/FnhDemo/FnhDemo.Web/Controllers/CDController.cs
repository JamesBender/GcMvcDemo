using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FnhDemo.Web.Models;
using FnhDemo.Web.Models.ViewModels;
using Track = FnhDemo.Data.Entities.Track;

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
            var cd = _cdModel.GetCd(id);
            return View(cd);
        }

        // GET: CD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CD/Create
        [HttpPost]
        public ActionResult Create(CD cd)
        {
            try
            {
                _cdModel.Save(cd);

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
            var cd = _cdModel.GetCd(id);
            return View(cd);
        }

        // POST: CD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CD cd)
        {
            try
            {
                _cdModel.Save(cd);

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
            var cd = _cdModel.GetCd(id);
            return View(cd);
        }

        // POST: CD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CD cd)
        {
            try
            {
                _cdModel.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
