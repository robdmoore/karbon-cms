using System.Collections.Generic;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Web.Embed;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    [KarbonTextTag("embed")]
    public class EmbedTag : AbstractKarbonTextTag
    {
        /// <summary>
        /// Gets the markup for the tag based upon the passed in parameters.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(IContent currentPage, IDictionary<string, string> parameters)
        {
            var url = parameters["embed"];

            parameters.Remove("embed");

            return EmbedProviderFactory.Instance.GetMarkup(url, parameters);
        }
    }
}
