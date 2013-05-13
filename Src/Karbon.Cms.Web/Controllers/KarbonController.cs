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
    /// <summary>
    /// The base class for Karbon based controllers.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class KarbonController<TModel> : Controller
        where TModel : IContent
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TModel Model
        {
            //TODO: Rename Content inline with other aireas?
            get { return (TModel)RouteData.Values[KarbonContentRoute.ModelKey]; }
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            var modelTypeName = Model.TypeName;
            var viewName = ViewExists(modelTypeName)
                ? modelTypeName
                : "Index";

            //TODO: Handle allowed views?

            return View(viewName, Model);
        }

        /// <summary>
        /// Determins whether a view exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private bool ViewExists(string name)
        {
            var result = ViewEngines.Engines.FindView(ControllerContext, name, null);
            return (result.View != null);
        }
    }

    public class KarbonController : KarbonController<Content>
    { }
}
