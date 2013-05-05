using System.Collections.Generic;

namespace Karbon.Core.Models
{
    public class Site : ISite
    {
        public IDictionary<string, string> Data { get; private set; }

        public Site()
        {
            Data = new Dictionary<string, string>();
        }
    }
}
