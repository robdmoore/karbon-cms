using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Configuration
{
    public class FileSystemProviderElement : ConfigurationElement
    {
        private const string ALIAS_KEY = "alias";
        private const string TYPE_KEY = "type";
        private const string PARAMETERS_KEY = "parameters";

        [ConfigurationProperty(ALIAS_KEY, IsKey = true, IsRequired = true)]
        public string Alias
        {
            get
            {
                return ((string)(base[ALIAS_KEY]));
            }
        }

        [ConfigurationProperty(TYPE_KEY, IsKey = false, IsRequired = true)]
        public string Type
        {
            get
            {
                return ((string)(base[TYPE_KEY]));
            }
        }

        [ConfigurationProperty(PARAMETERS_KEY, IsDefaultCollection = true, IsRequired = false)]
        public KeyValueConfigurationCollection Parameters
        {
            get
            {
                return ((KeyValueConfigurationCollection)(base[PARAMETERS_KEY]));
            }
        }
    }
}
