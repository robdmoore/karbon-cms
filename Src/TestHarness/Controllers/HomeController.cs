using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Karbon.Core.Models;
using TestHarness.Models;

namespace TestHarness.Controllers
{
    public class HomeController : Controller
    {
        private Home _model;

        public HomeController()
        {
            
        }

        public HomeController(IContent model)
        {
            _model = model as Home;
        }

        public ActionResult Index()
        {
            return View(_model);
        }
    }
}