using System.Collections.Generic;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    [KarbonTextTag("twitter")]
    public class TwitterTag : LinkTag
    {
        /// <summary>
        /// Gets the markup for the tag based upon the passed in parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(IContent currentPage, IDictionary<string, string> parameters)
        {
            parameters.Add("link", "http://twitter.com/" + parameters["twitter"]);

            if(!parameters.ContainsKey("text"))
                parameters.Add("text", "@" + parameters["twitter"]);

            if (!parameters.ContainsKey("target"))
                parameters.Add("target", "_blank");

            return base.GetMarkup(currentPage, parameters);
        }
    }
}
