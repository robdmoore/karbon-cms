using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.IO
{
    internal class IOHelper
    {
        public static string MapPath(string path)
        {
            return KarbonAppContext.Current.Environment.MapPath(path);
        }
    }
}
