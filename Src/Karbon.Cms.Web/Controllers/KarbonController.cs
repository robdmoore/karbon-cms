using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Web.Routing;

namespace Karbon.Cms.Web.Controllers
{
    public class KarbonController<TModel> : Controller
        where TModel : IContent
    {
        private readonly TModel _model;

        public TModel Model
        {
            get { return _model; }
        }

        public KarbonController(TModel model)
        {
            _model = model;
        }

        public virtual ActionResult Index()
        {
            var modelTypeName = Model.GetType().Name;
            var viewName = ViewExists(modelTypeName)
                ? modelTypeName
                : "Index";

            return View(viewName, Model);
        }

        private bool ViewExists(string name)
        {
            var result = ViewEngines.Engines.FindView(ControllerContext, name, null);
            return (result.View != null);
        }
    }

    public class KarbonController : KarbonController<IContent>
    {
        public KarbonController(IContent model)
            : base(model)
        { }
    }
}
