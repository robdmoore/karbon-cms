using System.Collections.Generic;

namespace Karbon.Cms.Core.Models
{
    public interface IContent : IEntity
    {
        IEnumerable<IFile> Files { get; set; }
    }
}
