using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Parsers;
using Karbon.Cms.Core.Stores;
using Karbon.Cms.Web.Embed;

namespace Karbon.Cms.Web.Tags
{
    [KarbonTextTag("embed")]
    public class EmbedTag : AbstractKarbonTextTag
    {
        /// <summary>
        /// Parses the tag based upon the specified parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string Parse(IContent currentPage, IDictionary<string, string> parameters)
        {
            var url = parameters["embed"];

            parameters.Remove("embed");

            return EmbedProviderFactory.Instance.GetMarkup(url, parameters);
        }
    }
}
