using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Karbon.Web.Model;

namespace Karbon.Web.Routing
{
    public class PageRoute : Route
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

        public static string DefaultController
        {
            get { return "karbon"; }
        }

        public static string DefaultAction
        {
            get { return "index"; }
        }

        public static string PageModelKey
        {
            get { return "currentPage"; }
        }

        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            var routeData = new RouteData(this, _routeHandler);
            var virtualPath = httpContext.Request.CurrentExecutionFilePath
                .TrimStart(new[] { '/' });

            var controller = DefaultController;
            var action = DefaultAction;

            IPageModel pageModel = null;
            if (string.IsNullOrEmpty(virtualPath) || string.Equals(virtualPath, "/"))
            {
                // Homepage request
            }
            else
            {
                // Content page request
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var model = values[PageModelKey] as IPageModel;
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

        public PageRoute(string url, 
            IRouteHandler routeHandler) 
            : base(url, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public PageRoute(string url, 
            RouteValueDictionary defaults, 
            IRouteHandler routeHandler) 
            : base(url, defaults, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public PageRoute(string url, 
            RouteValueDictionary defaults, 
            RouteValueDictionary constraints, 
            IRouteHandler routeHandler) 
            : base(url, defaults, constraints, routeHandler)
        {
            _url = url;
            _routeHandler = routeHandler;
        }

        public PageRoute(string url, 
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
