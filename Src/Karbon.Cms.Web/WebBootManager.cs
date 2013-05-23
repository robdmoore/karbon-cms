using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Web.Hosting;
using Karbon.Cms.Web.Modules;
using Karbon.Cms.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Karbon.Cms.Web
{
    public class WebBootManager : CoreBootManager
    {
        private bool _appStartingFlag;
        private bool _appStartedFlag;

        /// <summary>
        /// Initializes components that need to run before the application has started
        /// </summary>
        public override void AppStarting()
        {
            if (_appStartingFlag)
                throw new InvalidOperationException("The boot manager has already started");

            base.AppStarting();

            // Register http modules
            RegisterModules();

            _appStartingFlag = true;
        }

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public override void AppStarted()
        {
            if (_appStartedFlag)
                throw new InvalidOperationException("The boot manager has already started");

            base.AppStarted();

            // Wrap the http context
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);

            // Create the web context
            KarbonWebContext.Current = new KarbonWebContext(httpContextBase);

            // Reset the environment context
            KarbonAppContext.Current.Environment = new WebEnvironmentContext(httpContextBase);

            // Register the media VPP
            HostingEnvironment.RegisterVirtualPathProvider(new MediaVirtualPathProvider());

            // Register required routes
            RegisterRoutes();

            _appStartedFlag = true;
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        protected virtual void RegisterRoutes()
        {
            // Add the content route
            RouteTable.Routes.Insert(0, 
                new KarbonRoute(
                    "{*path}",
                    new RouteValueDictionary(new
                    {
                        controller = "Karbon", 
                        action = "Index"
                    }),
                    new MvcRouteHandler()));

            // Ignore media routes, these will be handled by the media VPP
            RouteTable.Routes.Ignore("media/{*path}");

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
