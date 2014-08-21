using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Serialization;

namespace Karbon.Cms.Core.Configuration
{
    internal class DataSerializersSection : ConfigurationSection
    {
        [ConfigurationProperty("providers", IsDefaultCollection = true, IsRequired = true)]
        public ProviderSettingsCollection Providers
        {
            get { return ((ProviderSettingsCollection)base["providers"]); }
        }

        [ConfigurationProperty("default", DefaultValue = "karbon")]
        public string Default
        {
            get { return ((string)base["default"]); }
            set { base["default"] = value; }
        }
    }
}
