using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Serialization
{
    public class PageDataSerializerCollection : ProviderCollection
    {
        new public PageDataSerializer this[string name]
        {
            get { return (PageDataSerializer)base[name]; }
        }
    }
}
