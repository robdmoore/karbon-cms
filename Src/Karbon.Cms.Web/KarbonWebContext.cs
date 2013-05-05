using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Karbon.Core;
using Karbon.Web.Routing;

namespace Karbon.Web
{
    public class KarbonWebContext
    {
        #region Factory

        private const string HttpContextItemName = "Karbon.Web.KarbonWebContext";
        private static readonly object Locker = new object();

        private static KarbonWebContext _context;
        public static KarbonWebContext Current
        {
            get
            {
                if (System.Web.HttpContext.Current != null)
                    return (KarbonWebContext)System.Web.HttpContext.Current.Items[HttpContextItemName];
                
                return _context;
            }
            set 
            {
                lock (Locker)
                {
                    // if running in a real HttpContext, this can only be set once
                    if (System.Web.HttpContext.Current != null && Current != null)
                        throw new ApplicationException("The current KarbonWebContext can only be set once during a request.");

                    // if there is an HttpContext, return the item
                    if (System.Web.HttpContext.Current != null)
                    {
                        System.Web.HttpContext.Current.Items[HttpContextItemName] = value;
                    }
                    else
                    {
                        _context = value;
                    }
                }
            }
        }

        #endregion

        public HttpContextBase HttpContext { get; private set; }

        public PageRequest PageRequest { get; set; }

        internal KarbonWebContext(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            HttpContext = httpContext;
        }
    }
}
