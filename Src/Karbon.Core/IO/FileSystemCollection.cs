using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.IO
{
    public class FileSystemCollection : ProviderCollection
    {
        new public FileSystem this[string name]
        {
            get { return (FileSystem)base[name]; }
        }
    }
}
