using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    public static class FileApiExtensions
    {
        /// <summary>
        /// Gets the absolute url for the given file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static string Url(this IFile file)
        {
            return VirtualPathUtility.ToAbsolute(file.RelativeUrl);
        }
    }
}
