using System.Collections.Generic;

namespace Karbon.Core.Models
{
    public interface ISite
    {
        IDictionary<string, string> Data { get; }
    }
}
