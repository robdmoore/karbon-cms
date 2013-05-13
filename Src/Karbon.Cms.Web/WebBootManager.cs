using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Web.Modules;
using Karbon.Cms.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Karbon.Cms.Web
{
    public class WebBootManager : CoreBootManager
    {
        /// <summary>
        /// Initializes components that need to run before the application has started
        /// </summary>
        public override void AppStarting()
        {
            base.AppStarting();

            // Register http modules
            RegisterModules();
        }

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public override void AppStarted()
        {
            base.AppStarted();

            // Wrap the http context
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);

            // Create the web context
            KarbonWebContext.Current = new KarbonWebContext(httpContextBase);

            // Reset the environment context
            KarbonAppContext.Current.Environment = new WebEnvironmentContext(httpContextBase);

            // Register required routes
            RegisterRoutes();
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        protected virtual void RegisterRoutes()
        {
            // Add the content route
            RouteTable.Routes.Insert(0, 
                new KarbonContentRoute(
                    "{*path}",
                    new RouteValueDictionary(new
                    {
                        controller = "Karbon", 
                        action = "Index"
                    }),
                    new MvcRouteHandler()));

            // Add the media route
            RouteTable.Routes.Insert(0,
                new Route("media/{*path}", 
                    new KarbonMediaHandler()));

        }

        /// <summary>
        /// Registers the modules.
        /// </summary>
        protected virtual void RegisterModules()
        {
            DynamicModuleUtility.RegisterModule(typeof(KarbonRequestModule));
        }
    }
}
