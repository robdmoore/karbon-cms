using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Models
{
    public interface IRoutableEntity : IEntity
    {
        string Slug { get; set; }
        string RelativeUrl { get; set; }
        int Depth { get; set; }
    }
}
