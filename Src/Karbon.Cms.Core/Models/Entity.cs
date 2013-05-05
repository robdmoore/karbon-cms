using System;
using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public abstract class Entity : IEntity
    {
        public virtual string Path { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
        public virtual IDictionary<string, string> Data { get; set; }

        protected Entity()
        {
            Data = new Dictionary<string, string>();
        }
    }
}
