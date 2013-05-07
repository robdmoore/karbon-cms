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

        public static string PageModelKey
        {
            get { return "currentPage"; }
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
            var routeData = new RouteData(this, _routeHandler);
            var virtualPath = httpContext.Request.CurrentExecutionFilePath
                .TrimStart(new[] { '/' });

            var action = DefaultAction;

            // Try and grab the model for the given URL
            var model = StoreManager.ContentStore.GetByUrl(virtualPath);

            if (model == null && virtualPath.LastIndexOf("/", StringComparison.InvariantCulture) > 0)
            {
                // Try to load the page without the last segment of the url and set the last segment as action
                var index = virtualPath.LastIndexOf("/", StringComparison.InvariantCulture);

                var tmpAction = virtualPath.Substring(index, virtualPath.Length - index).Trim(new[] { '/' });
                var tmpVirtualPath = virtualPath.Substring(0, index).TrimStart(new[] { '/' });

                model = StoreManager.ContentStore.GetByUrl(tmpVirtualPath);
                if(model != null)
                {
                    virtualPath = tmpVirtualPath;
                    action = tmpAction;
                }
            }

            if(model == null)
            {
                // If the model still is empty, let's try to resolve if the start page has an action named (virtualUrl)
                model = StoreManager.ContentStore.GetByUrl("");
                if(model != null)
                {
                    action = virtualPath;
                }
            }

            if (model == null)
            {
                return null;
            }

            // We have a model, so lets work out where to direct the request
            var contentAttr = model.GetType().GetCustomAttribute<ContentAttribute>();
            var controllerName = (contentAttr != null && contentAttr.ControllerType != null)
                ? contentAttr.ControllerType.Name
                : string.Format("{0}Controller", model.GetType().Name);

            var controllers = TypeFinder.FindTypes<Controller>().ToList();
            var controller = controllers.SingleOrDefault(x => x.Name == controllerName) ??
                             controllers.SingleOrDefault(x => x.Name == DefaultController);

            if(controller == null || controller.GetMethods().All(x => x.Name.ToLower() != action.ToLower()))
            {
                return null;
            }

            routeData.Values[ControllerKey] = controller.Name.Replace("Controller", "");
            routeData.Values[ActionKey] = action;
            routeData.Values[PageModelKey] = model;
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var model = values[PageModelKey] as IContent;
            if (model == null)
                return null;

            var vpd = new VirtualPathData(this, model.Url);

            var queryParams = values.Where(kvp => !kvp.Key.Equals(PageModelKey) 
                && !kvp.Key.Equals(ControllerKey) 
                && !kvp.Key.Equals(ActionKey))
                .ToQueryString();
            
            vpd.VirtualPath += queryParams;

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
