using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface ISite
    {
        IDictionary<string, string> Data { get; }
    }
}
