using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Configuration
{
    [ConfigurationCollection(typeof(FileSystemProviderElement), AddItemName = "provider")]
    public class FileSystemProviderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileSystemProviderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileSystemProviderElement)(element)).Alias;
        }

        new public FileSystemProviderElement this[string key]
        {
            get
            {
                return (FileSystemProviderElement)BaseGet(key);
            }
        }
    }
}
