using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    /// <summary>
    /// Contains API content methods that are dependant on the web context
    /// </summary>
    public static class ContentApiExtensions
    {
        /// <summary>
        /// Determines whether the specified content is open.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is open; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOpen(this IContent content)
        {
            // TODO: Parse URL or check current route
            return false;
        }
    }
}
