using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Karbon.Core.Configuration;

namespace Karbon.Core.IO
{
    public class FileStoreManager
    {
        private static FileStoreCollection _providers;

        static FileStoreManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            // Parse config
            var config = (FileStoresSection)ConfigurationManager.GetSection("karbon/fileStores");
            if (config == null)
                throw new ConfigurationErrorsException("fileStores configuration section is not set correctly.");

            // Create providers
            _providers = new FileStoreCollection();

            ProvidersHelper.InstantiateProviders(config.Providers, 
                _providers, typeof(FileStore));

            _providers.SetReadOnly();
        }

        public static FileStoreCollection Providers
        {
            get { return _providers; }
        }

        public static FileStore ContentFileStore
        {
            get { return _providers["content"]; }
        }

        public static FileStore MediaFileStore
        {
            get { return _providers["media"]; }
        }
    }
}
