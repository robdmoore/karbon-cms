using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Stores
{
    public class StoreManager
    {
        /// <summary>
        /// Gets or sets the content store.
        /// </summary>
        /// <value>
        /// The content store.
        /// </value>
        public static IContentStore ContentStore { get; set; }

        /// <summary>
        /// Gets or sets the media store.
        /// </summary>
        /// <value>
        /// The media store.
        /// </value>
        public static IMediaStore MediaStore { get; set; }
    }
}
