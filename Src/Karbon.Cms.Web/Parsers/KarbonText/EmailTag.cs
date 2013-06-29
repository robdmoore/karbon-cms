using System;
using System.Collections.Generic;
using System.Text;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    [KarbonTextTag("email")]
    public class EmailTag : AbstractKarbonTextTag
    {
        /// <summary>
        /// Gets the markup for the tag based upon the passed in parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(IContent currentPage, IDictionary<string, string> parameters)
        {
            var email = parameters["email"];
            var text = email;

            if (parameters.ContainsKey("subject"))
                email += ((email.IndexOf("?", StringComparison.InvariantCulture) == -1) ? "?" : "&") + "subject=" + parameters["subject"];

            if (parameters.ContainsKey("body"))
                email += ((email.IndexOf("?", StringComparison.InvariantCulture) == -1) ? "?" : "&") + "body=" + parameters["body"];

            var sb = new StringBuilder();
            sb.AppendFormat("<a href=\"mailto:{0}\"", email); // TODO: HTML encode

            if (parameters.ContainsKey("title"))
                sb.AppendFormat(" title=\"{0}\"", parameters["title"]);

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
}
