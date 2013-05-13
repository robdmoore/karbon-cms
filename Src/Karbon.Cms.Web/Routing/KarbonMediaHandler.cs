using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Karbon.Cms.Web.Routing
{
    /// <summary>
    /// Responsible for proxying media requests
    /// </summary>
    public class KarbonMediaHandler : IHttpHandler, IRouteHandler
    {
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ProcessRequest(HttpContext context)
        {
            // TODO: Parse URL and return media item
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
        public bool IsReusable { get { return false; } }

        /// <summary>
        /// Provides the object that processes the request.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the request.</param>
        /// <returns>
        /// An object that processes the request.
        /// </returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
}
