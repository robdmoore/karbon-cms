using System;
using System.Collections.Generic;

namespace Karbon.Core.Models
{
    public interface IEntity
    {
        string Path { get; }
        string Slug { get; }
        string Url { get; }

        DateTime Created { get; }
        DateTime Modified { get; }

        IDictionary<string, string> Data { get; }
    }
}
