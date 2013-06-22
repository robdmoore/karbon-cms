using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Qik", @"qik\.com/")]
    public class QikEmbedProvider : AbstractVideoEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://qik.com/api/oembed.xml"; }
        }

        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override string GetMarkup(string url, IDictionary<string,string> parameters)
        {
            var requestUrl = BuildRequestUrl(url, parameters);
            var responseXml = GetXmlResponse(requestUrl);
            var selectSingleNode = responseXml.SelectSingleNode("/hash/html");

            return selectSingleNode != null
                ? selectSingleNode.InnerText
                : null;
        }
    }
}
