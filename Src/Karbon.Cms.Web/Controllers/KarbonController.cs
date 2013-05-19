using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;
using Karbon.Cms.Web.Routing;

namespace Karbon.Cms.Web.Controllers
{
    /// <summary>
    /// The base class for Karbon based controllers.
    /// </summary>
    /// <typeparam name="TCurrentPageType">The type of the current page.</typeparam>
    /// <typeparam name="THomePageType">The type of the home page.</typeparam>
    public class KarbonController<TCurrentPageType, THomePageType> : Controller
        where TCurrentPageType : IContent
        where THomePageType : IContent
    {
        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public TCurrentPageType CurrentPage
        {
            get { return (TCurrentPageType)RouteData.Values[KarbonRoute.ModelKey]; }
        }

        /// <summary>
        /// Gets the home page.
        /// </summary>
        /// <value>
        /// The home page.
        /// </value>
        public THomePageType HomePage
        {
            get { return (THomePageType)StoreManager.ContentStore.GetByUrl("~/"); }
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            var modelTypeName = CurrentPage.TypeName;
            var viewName = ViewExists(modelTypeName)
                ? modelTypeName
                : "Index";

            //TODO: Handle allowed views?

            return View(viewName, CurrentPage);
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

    /// <summary>
    /// The base class for Karbon based controllers.
    /// </summary>
    /// <typeparam name="TCurrentPageType">The type of the current page type.</typeparam>
    public class KarbonController<TCurrentPageType> : KarbonController<TCurrentPageType, Content>
        where TCurrentPageType : IContent
    { }

    /// <summary>
    /// The base class for Karbon based controllers.
    /// </summary>
    public class KarbonController : KarbonController<Content, Content>
    { }
}
