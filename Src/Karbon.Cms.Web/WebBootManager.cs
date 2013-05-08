using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Web.Mvc;
using Karbon.Cms.Web.Routing;

namespace Karbon.Cms.Web
{
    public class WebBootManager : CoreBootManager
    {
        public override void Initialize()
        {
            base.Initialize();

            // Wrap the http context
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);

            // Create the web context
            KarbonWebContext.Current = new KarbonWebContext(httpContextBase);

            // Reset the environment context
            KarbonAppContext.Current.Environment = new WebEnvironmentContext(httpContextBase);

            // Register required routes
            RegisterRoutes();
        }

        protected virtual void RegisterRoutes()
        {
            RouteTable.Routes.Add("Default_Pages", 
                new KarbonRoute(
                    "{*path}",
                    new RouteValueDictionary(new
                    {
                        controller = "karbon", 
                        action = "index"
                    }),
                    new MvcRouteHandler()));
        }
    }
}
