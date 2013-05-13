using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Models
{
    public abstract class RoutableEntity : Entity, IRoutableEntity
    {
        public virtual string Slug { get; set; }
        public virtual string RelativeUrl { get; set; }
        public virtual int Depth { get; set; }
    }
}
