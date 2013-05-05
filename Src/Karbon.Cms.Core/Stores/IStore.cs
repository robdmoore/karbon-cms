using Karbon.Core.Models;

namespace Karbon.Core.Stores
{
    public interface IStore<TEntity>
        where TEntity : IEntity
    {
        TEntity GetByUrl(string url);
    }
}
