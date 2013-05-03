using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Karbon.Web.Extensions
{
    public static class RouteDataExtensions
    {
        public static RouteData SetCurrentPage(this RouteData routeData,
            string controllerName,
            string actionName,
            dynamic model)
        {
            return routeData;
        }
    }
}
