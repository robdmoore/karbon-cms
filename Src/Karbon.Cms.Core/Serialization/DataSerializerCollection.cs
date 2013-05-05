using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Serialization
{
    public class DataSerializerCollection : ProviderCollection
    {
        new public DataSerializer this[string name]
        {
            get { return (DataSerializer)base[name]; }
        }
    }
}
