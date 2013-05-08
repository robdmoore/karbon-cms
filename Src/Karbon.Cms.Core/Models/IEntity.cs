using System;
using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface IEntity
    {
        string Path { get; set; }
        string TypeName { get; set; }
        string Slug { get; set; }
        string Url { get; set; }
        int SortOrder { get; set; }

        DateTimeOffset Created { get; set; }
        DateTimeOffset Modified { get; set; }

        IDictionary<string, string> Data { get; set; }
    }
}
