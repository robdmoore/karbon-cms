using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Karbon.Cms.Web.Controllers
{
    public class KarbonController : Controller
    {
        public ActionResult Index()
        {
            var model = ControllerContext.RouteData.Values["currentPage"];
            var type = model.GetType();

            //TODO: Need to workout valid views?

            return View(type.Name, model);
        }
    }
}
