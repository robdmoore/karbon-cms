using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface IContent : IRoutableEntity
    {
        IEnumerable<IFile> Files { get; set; }
    }
}
