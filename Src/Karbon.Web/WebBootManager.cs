using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Core;
using Karbon.Web.Routing;

namespace Karbon.Web
{
    public class WebBootManager : CoreBootManager
    {
        public override void Initialize()
        {
            base.Initialize();

            KarbonWebContext.Current = new KarbonWebContext(
                new HttpContextWrapper(HttpContext.Current));

            RegisterRoutes();
        }

        protected virtual void RegisterRoutes()
        {
            RouteTable.Routes.Add("Default_Pages", 
                new PageRoute(
                    "{*path}",
                    new RouteValueDictionary(new
                    {
                        controller = "pages", 
                        action = "index"
                    }),
                    new MvcRouteHandler()));
        }
    }
}
