using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Karbon.Core.Configuration;

namespace Karbon.Core.Serialization
{
    public class PageDataSerializerManager
    {
        private static PageDataSerializer _defaultProvider;
        private static PageDataSerializerCollection _providers;

        static PageDataSerializerManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            // Parse config
            var config = (PageDataSerializersSection)ConfigurationManager.GetSection("karbon/pageDataSerializers");
            if (config == null)
                throw new ConfigurationErrorsException("pageDataSerializers configuration section is not set correctly.");

            // Create providers
            _providers = new PageDataSerializerCollection();

            ProvidersHelper.InstantiateProviders(config.Providers,
                _providers, typeof(PageDataSerializer));

            _providers.SetReadOnly();

            // Get default provider
            _defaultProvider = _providers[config.Default];

            if (_defaultProvider == null)
                throw new Exception("_defaultProvider");
        }

        public static PageDataSerializerCollection Providers
        {
            get { return _providers; }
        }

        public static PageDataSerializer Default
        {
            get { return _defaultProvider; }
        }
    }
}
