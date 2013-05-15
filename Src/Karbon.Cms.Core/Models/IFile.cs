namespace Karbon.Cms.Core.Models
{
    public interface IFile : IEntity
    {
        string ContentRelativeUrl { get; set; }
        string Extension { get; set; }
    }
}
