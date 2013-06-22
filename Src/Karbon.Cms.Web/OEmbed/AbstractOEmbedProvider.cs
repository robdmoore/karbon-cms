using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Karbon.Cms.Web.OEmbed
{
    public abstract class AbstractOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public abstract string ApiEndpoint { get; }

        /// <summary>
        /// Gets the default parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public virtual IDictionary<string, string> Parameters 
        { 
            get { return new Dictionary<string, string>(); }
        }

        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual string GetMarkup(string url, IDictionary<string, string> parameters)
        {
            var requestUrl = BuildRequestUrl(url, parameters);
            var responseXml = GetXmlResponse(requestUrl);
            var selectSingleNode = responseXml.SelectSingleNode("/oembed/html");

            return selectSingleNode != null 
                ? selectSingleNode.InnerText 
                : null;
        }

        /// <summary>
        /// Builds the request URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected virtual string BuildRequestUrl(string url, IDictionary<string, string> parameters)
        {
            var fullUrl = new StringBuilder();

            fullUrl.Append(ApiEndpoint);
            fullUrl.Append("?url=" + url);

            foreach (var p in parameters)
                fullUrl.Append(string.Format("&{0}={1}", p.Key, p.Value));

            foreach (var p in Parameters.Where(x => !parameters.ContainsKey(x.Key)))
                fullUrl.Append(string.Format("&{0}={1}", p.Key, p.Value));

            return fullUrl.ToString();
        }

        /// <summary>
        /// Gets the XML response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        protected virtual XmlDocument GetXmlResponse(string url)
        {
            var webClient = new System.Net.WebClient();
            var response = webClient.DownloadString(url);

            var doc = new XmlDocument();
            doc.LoadXml(response);

            return doc;
        }
    }
}
