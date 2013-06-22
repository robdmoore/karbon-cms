using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.OEmbed
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class OEmbedProviderAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL scheme regex.
        /// </summary>
        /// <value>
        /// The URL scheme regex.
        /// </value>
        public string UrlSchemeRegex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OEmbedProviderAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="urlSchemeRegex">The URL scheme regex.</param>
        public OEmbedProviderAttribute(string name, string urlSchemeRegex)
        {
            Name = name;
            UrlSchemeRegex = urlSchemeRegex;
        }
    }
}
