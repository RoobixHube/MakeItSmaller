using MakeItSmaller.DataLayer;
using MakeItSmaller.Models;
using MakeItSmaller.Services;
using System;
using System.IO;
using System.Web.Mvc;

namespace MakeItSmaller.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMISURLService _service;
        private const string dbFileName = "MISURLdb.txt";

        public HomeController()
        {
            string systemPath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(systemPath, dbFileName);

            IMISURLRepository _repository = new MISURLRepository(path);
            _service = new MISURLService(_repository);
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult MakeItSmaller()
        {
            ViewBag.Message = "Make It Smaller";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeItSmaller(URL userURL)
        {
            if (ModelState.IsValid)
            {
                if (_service.CreateMISURLFromURL(userURL.URLstring))
                {
                    var smallerURL = _service.GetMISURLFromURL(userURL.URLstring);

                    var newMISURL = new MISURLCouple()
                    {
                        URL = userURL.URLstring,
                        MISURL = "https://localhost:44310/" + smallerURL
                    };

                    TempData["MISURLCouple"] = newMISURL;

                    return RedirectToAction("ViewMakeItSmaller", newMISURL);
                }
            }

            return View();
        }


        public ActionResult ViewMakeItSmaller(MISURLCouple couple)
        {
            return View(couple);
        }
    }
}
