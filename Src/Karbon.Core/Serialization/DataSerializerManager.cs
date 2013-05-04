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
    public class DataSerializerManager
    {
        private static DataSerializer _defaultProvider;
        private static DataSerializerCollection _providers;

        static DataSerializerManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            // Parse config
            var config = (DataSerializersSection)ConfigurationManager.GetSection("karbon/dataSerializers");
            if (config == null)
                throw new ConfigurationErrorsException("dataSerializers configuration section is not set correctly.");

            // Create providers
            _providers = new DataSerializerCollection();

            ProvidersHelper.InstantiateProviders(config.Providers,
                _providers, typeof(DataSerializer));

            _providers.SetReadOnly();

            // Get default provider
            _defaultProvider = _providers[config.Default];

            if (_defaultProvider == null)
                throw new Exception("_defaultProvider");
        }

        public static DataSerializerCollection Providers
        {
            get { return _providers; }
        }

        public static DataSerializer Default
        {
            get { return _defaultProvider; }
        }
    }
}
