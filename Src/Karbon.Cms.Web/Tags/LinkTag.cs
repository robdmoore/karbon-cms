using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Parsers;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Tags
{
    [KarbonTextTag("link")]
    public class LinkTag : IKarbonTextTag
    {
        /// <summary>
        /// Parses the tag based upon the specified parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual string Parse(IContent currentPage, IDictionary<string, string> parameters)
        {
            var url = parameters["link"]; 
            var text = url;

            if (!url.StartsWith("http") && !url.StartsWith("/"))
            {
                // Try to find matching content
                var content = StoreManager.ContentStore.GetByUrl("~/" + url);
                if (content != null)
                {
                    url = content.Url();
                    text = content.Name;
                }
                else
                {
                    // Try to find matching file
                    var file = currentPage.AllFiles.SingleOrDefault(x => x.Slug == url);
                    if (file != null)
                    {
                        url = file.Url();
                        text = file.Name;
                    }
                }
            }

            var sb = new StringBuilder();
            sb.AppendFormat("<a href=\"{0}\"", url);

            if (parameters.ContainsKey("title"))
                sb.AppendFormat(" title=\"{0}\"", parameters["title"]);

            if (parameters.ContainsKey("target"))
                sb.AppendFormat(" target=\"{0}\"", parameters["target"]);

            if (parameters.ContainsKey("class"))
                sb.AppendFormat(" class=\"{0}\"", parameters["class"]);

            sb.Append(">");
            sb.Append(parameters.ContainsKey("text")
                ? parameters["text"]
                : text);

            sb.Append("</a>");

            return sb.ToString();
        }
    }

    [KarbonTextTag("a")]
    public class ATag : LinkTag
    {
        /// <summary>
        /// Parses the tag based upon the specified parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string Parse(IContent currentPage, IDictionary<string, string> parameters)
        {
            parameters.AddOrUpdate("link", parameters["a"]);

            return base.Parse(currentPage, parameters);
        }
    }
}
