using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Karbon.Web.Controllers
{
    public class KarbonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
