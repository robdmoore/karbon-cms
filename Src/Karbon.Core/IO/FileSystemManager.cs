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
    public class FileSystemManager
    {
        private static FileSystemCollection _providers;

        static FileSystemManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            // Parse config
            var config = (FileSystemsSection)ConfigurationManager.GetSection("karbon/fileSystems");
            if (config == null)
                throw new ConfigurationErrorsException("fileSystems configuration section is not set correctly.");

            // Create providers
            _providers = new FileSystemCollection();

            ProvidersHelper.InstantiateProviders(config.Providers, 
                _providers, typeof(FileSystem));

            _providers.SetReadOnly();
        }

        public static FileSystemCollection Providers
        {
            get { return _providers; }
        }
    }
}
