using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Parsers;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web.Tags
{
    [KarbonTextTag("twitter")]
    public class TwitterTag : LinkTag
    {
        /// <summary>
        /// Parses the tag based upon the specified parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string Parse(IContent currentPage, IDictionary<string, string> parameters)
        {
            parameters.Add("link", "http://twitter.com/" + parameters["twitter"]);

            if(!parameters.ContainsKey("text"))
                parameters.Add("text", "@" + parameters["twitter"]);

            if (!parameters.ContainsKey("target"))
                parameters.Add("target", "_blank");

            return base.Parse(currentPage, parameters);
        }
    }
}
