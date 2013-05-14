using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Stores;

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
            // Parse URL and return media item
            var url = context.Request.Url.LocalPath;
            var fileRelativeUrl = url.Substring(url.LastIndexOf("/media/") + 7);
            var fileSlug = fileRelativeUrl;
            var contentRelativeUrl = "";

            if(fileRelativeUrl.LastIndexOf('/') > -1)
            {
                fileSlug = fileRelativeUrl.Substring(fileRelativeUrl.LastIndexOf('/') + 1);
                contentRelativeUrl = fileRelativeUrl.Substring(0, fileRelativeUrl.LastIndexOf('/'));
            }

            var content = StoreManager.ContentStore.GetByUrl("~/" + contentRelativeUrl);
            if(content == null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var file = content.AllFiles.SingleOrDefault(x => x.Slug == fileSlug);
            if(file == null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var fileStream = FileStoreManager.Default.OpenFile(file.RelativePath);
            var fileStreamLength = (int)fileStream.Length;
            var bytes = fileStreamLength;

            context.Response.Buffer = false;
            //TODO Work out content type
            context.Response.ContentType = "application/octet-stream";
            context.Response.AppendHeader("content-length", fileStreamLength.ToString());

            var buffer = new byte[1024];

            while (fileStreamLength > 0 && (bytes =
                fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, bytes);
                context.Response.Flush();
                fileStreamLength -= bytes;
            }

            fileStream.Close();

            context.Response.Close();
            context.Response.End();
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
