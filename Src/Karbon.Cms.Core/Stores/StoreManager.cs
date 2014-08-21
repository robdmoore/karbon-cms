using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Stores
{
    internal class StoreManager
    {
        /// <summary>
        /// Gets or sets the content store.
        /// </summary>
        /// <value>
        /// The content store.
        /// </value>
        public static IContentStore ContentStore { get; set; }
    }
}
