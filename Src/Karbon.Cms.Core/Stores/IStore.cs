using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Stores
{
    public interface IStore<TEntity>
        where TEntity : IEntity
    {
        TEntity GetByUrl(string url);
    }
}
