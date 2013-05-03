using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Configuration
{
    public class FileSystemProvidersSection : ConfigurationSection
    {
        private const string PROVIDERS_KEY = "providers";

        [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public FileSystemProviderElementCollection Providers
        {
            get { return ((FileSystemProviderElementCollection)(base[""])); }
        }
    }
}
