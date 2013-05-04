using System;
using System.Collections.Generic;

namespace Karbon.Core.Models
{
    public class Content : IContent
    {
        public virtual string Path { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual IDictionary<string, string> Data { get; set; }

        public Content()
        {
            Data = new Dictionary<string, string>();
        }
    }
}
