using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Core.IO;

namespace Karbon.Core.Configuration
{
    public class FileStoresSection : ConfigurationSection
    {
        [ConfigurationProperty("providers", IsDefaultCollection = true, IsRequired = true)]
        public ProviderSettingsCollection Providers
        {
            get { return ((ProviderSettingsCollection)base["providers"]); }
        }
    }
}
