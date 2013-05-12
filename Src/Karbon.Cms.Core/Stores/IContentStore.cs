using System.Collections.Generic;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Stores
{
    internal interface IContentStore : IStore<IContent>
    {
        IEnumerable<IContent> GetAll();

        IContent GetParent(IContent content);
        IEnumerable<IContent> GetChildren(IContent content); 

        void SyncCache();
    }
}
