using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Routing
{
    public class KarbonRoute : Route
    {
        private string _url;
        private IRouteHandler _routeHandler;

        public static string ControllerKey 
        {
            get { return "controller"; }
        }

        public static string ActionKey
        {
            get { return "action"; }
        }

        public static string ModelKey
        {
            get { return "content"; }
        }

        public static string DefaultController
        {
            get { return "KarbonController"; }
        }

        public static string DefaultAction
        {
            get { return "Index"; }
        }

        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            // Setup the route
            var routeData = new RouteData(this, _routeHandler);
            var virtualPath = httpContext.Request.CurrentExecutionFilePath
                .TrimStart(new[] { '/' });

            var action = DefaultAction;

            // Try and grab the model for the given URL
            var model = StoreManager.ContentStore.GetByUrl(virtualPath);
            if (model == null)
            {
                // Try to load the page without the last segment of the url and set the last segment as action
                if (virtualPath.LastIndexOf("/", StringComparison.InvariantCulture) > 0)
                {
                    var index = virtualPath.LastIndexOf("/", StringComparison.InvariantCulture);

                    var tmpAction = virtualPath.Substring(index, virtualPath.Length - index).Trim(new[] {'/'});
                    var tmpVirtualPath = virtualPath.Substring(0, index).TrimStart(new[] {'/'});

                    model = StoreManager.ContentStore.GetByUrl(tmpVirtualPath);
                    if (model != null)
                    {
                        virtualPath = tmpVirtualPath;
                        action = tmpAction;
                    }
                }
                else
                {
                    model = StoreManager.ContentStore.GetByUrl("");
                    if (model != null)
                    {
                        action = virtualPath;
                    }
                }
            }

            // If by now we haven't found a model, let MVC see if there
            // is a better route registered
            if (model == null)
                return null;

            // We have a model, so lets work out where to direct the request
            var controller = model.GetController(DefaultController);
            if(controller == null || !controller.HasMethod(action))
                return null;

            // Route the request
            routeData.Values[ControllerKey] = controller.Name.TrimEnd("Controller");
            routeData.Values[ActionKey] = action;
            routeData.Values[ModelKey] = model;
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            // Grab the model from the route data collection
            var model = values[ModelKey] as IContent;
            if (model == null)
                return null;

            // Create virtual path from model url
            var vpd = new VirtualPathData(this, model.Url);

            // Append any other route data values as querystring params
            var queryParams = values.Where(kvp => !kvp.Key.Equals(ModelKey) 
                && !kvp.Key.Equals(ControllerKey) 
                && !kvp.Key.Equals(ActionKey))
                .ToQueryString();
            
            vpd.VirtualPath += queryParams;

            // Return the virtual path
            return vpd;
        }

        #region Constructors

        public KarbonRoute(string url, 
            IRouteHandler routeHandler) 
            : base(url, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            IRouteHandler routeHandler) 
            : base(url, defaults, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints, 
            IRouteHandler routeHandler) 
            : base(url, defaults, constraints, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public KarbonRoute(string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints, 
            RouteValueDictionary dataTokens, 
            IRouteHandler routeHandler) 
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        #endregion
    }
}
