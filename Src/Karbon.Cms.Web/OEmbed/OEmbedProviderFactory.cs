using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Parsers;

namespace Karbon.Cms.Web.OEmbed
{
    internal class OEmbedProviderFactory
    {
        private static readonly OEmbedProviderFactory _instance = new OEmbedProviderFactory();
        private IDictionary<string, Type> _providers;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static OEmbedProviderFactory Instance
        {
            get 
            {
                return _instance; 
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OEmbedProviderFactory"/> class.
        /// </summary>
        private OEmbedProviderFactory()
        {
            _providers = TypeFinder.FindTypes<AbstractOEmbedProvider>()
                .Where(x => x.GetCustomAttribute<OEmbedProviderAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<OEmbedProviderAttribute>().UrlSchemeRegex, x => x);
        }

        /// <summary>
        /// Gets the markup.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public string GetMarkup(string url, IDictionary<string, string> parameters)
        {
            var providerKey = _providers.Keys.FirstOrDefault(x => Regex.IsMatch(url, x, RegexOptions.IgnoreCase));
            if(providerKey != null)
            {
                var providerType = _providers[providerKey];
                var provider = Activator.CreateInstance(providerType) as AbstractOEmbedProvider;
                return provider.GetMarkup(url, parameters);
            }

            return string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url);
        }
    }
}
