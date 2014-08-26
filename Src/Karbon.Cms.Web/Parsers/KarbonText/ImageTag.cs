using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    [KarbonTextTag("image")]
    public class ImageTag : AbstractKarbonTextTag
    {
        /// <summary>
        /// Gets the markup for the tag based upon the passed in parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(IContent currentPage, IDictionary<string, string> parameters)
        {
            var src = parameters["image"];

            if (!src.StartsWith("http") && !src.StartsWith("/"))
            {
                var image = currentPage.Images().SingleOrDefault(x => x.Slug == src);
                if (image != null)
                {
                    src = image.Url();
                }
            }

            if (parameters.ContainsKey("width"))
                src += ((src.IndexOf("?", StringComparison.InvariantCulture) == -1) ? "?" : "&") + "width=" + parameters["width"];

            if (parameters.ContainsKey("height"))
                src += ((src.IndexOf("?", StringComparison.InvariantCulture) == -1) ? "?" : "&") + "height=" + parameters["height"];

            if (parameters.ContainsKey("mode"))
                src += ((src.IndexOf("?", StringComparison.InvariantCulture) == -1) ? "?" : "&") + "mode=" + parameters["mode"];

            var sb = new StringBuilder();
            sb.AppendFormat("<img src=\"{0}\"", src);

            if (parameters.ContainsKey("alt"))
                sb.AppendFormat(" alt=\"{0}\"", parameters["alt"]);

            if (parameters.ContainsKey("title"))
                sb.AppendFormat(" title=\"{0}\"", parameters["title"]);

            if (parameters.ContainsKey("class"))
                sb.AppendFormat(" class=\"{0}\"", parameters["class"]);

            if (parameters.ContainsKey("width"))
                sb.AppendFormat(" width=\"{0}\"", parameters["width"]);

            if (parameters.ContainsKey("height"))
                sb.AppendFormat(" height=\"{0}\"", parameters["height"]);

            sb.Append(" />");

            if(parameters.ContainsKey("link"))
            {
                parameters.AddOrUpdate("text", sb.ToString());
                return new LinkTag().GetMarkup(currentPage, parameters);
            }

            return sb.ToString();
        }
    }

    [KarbonTextTag("img")]
    public class ImgTag : ImageTag
    {
        /// <summary>
        /// Gets the markup for the tag based upon the passed in parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(IContent currentPage, IDictionary<string, string> parameters)
        {
            parameters.AddOrUpdate("image", parameters["img"]);

            return base.GetMarkup(currentPage, parameters);
        }
    }
}
