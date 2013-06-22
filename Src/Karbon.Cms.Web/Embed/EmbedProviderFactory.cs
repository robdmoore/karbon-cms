using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Parsers;

namespace Karbon.Cms.Web.Embed
{
    internal class EmbedProviderFactory
    {
        private static readonly EmbedProviderFactory _instance = new EmbedProviderFactory();
        private readonly IDictionary<string, Type> _providers;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static EmbedProviderFactory Instance
        {
            get 
            {
                return _instance; 
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EmbedProviderFactory"/> class.
        /// </summary>
        private EmbedProviderFactory()
        {
            _providers = TypeFinder.FindTypes<AbstractEmbedProvider>()
                .Where(x => x.GetCustomAttribute<EmbedProviderAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<EmbedProviderAttribute>().UrlSchemeRegex, x => x);
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
                var provider = Activator.CreateInstance(providerType) as AbstractEmbedProvider;
                return provider.GetMarkup(url, parameters);
            }

            return string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url);
        }
    }
}
