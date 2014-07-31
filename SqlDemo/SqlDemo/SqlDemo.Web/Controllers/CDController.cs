using System.Web.Mvc;
using SqlDemo.Web.Models;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Controllers
{
    public class CDController : Controller
    {
        private CDModel _cdModel;

        public CDController()
        {
            _cdModel = new CDModel();
        }

        // GET: CD
        public ActionResult Index()
        {
            var listOfCd = _cdModel.GetListOfAllCD();
            return View(listOfCd);
        }

        // GET: CD/Details/5
        public ActionResult Details(int id)
        {
            var cd = _cdModel.GetCdDetails(id);
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
            var cd = _cdModel.GetCdDetails(id);
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
            var cd = _cdModel.GetCdDetails(id);
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
