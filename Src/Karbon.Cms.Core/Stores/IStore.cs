using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Stores
{
    internal interface IStore<TEntity>
        where TEntity : IEntity
    {
        TEntity GetByUrl(string url);
    }
}
