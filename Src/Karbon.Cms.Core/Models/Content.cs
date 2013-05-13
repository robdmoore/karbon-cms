using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public class Content : RoutableEntity, IContent
    {
        public IEnumerable<IFile> Files { get; set; }
    }
}
