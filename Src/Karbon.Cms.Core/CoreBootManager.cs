using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Serialization;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Core
{
    public class CoreBootManager
    {
        private bool _isInitialized;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The boot manager has already been initialized</exception>
        public virtual void Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException("The boot manager has already been initialized");

            KarbonAppContext.Current = new KarbonAppContext();

            StoreManager.ContentStore = new ContentStore();
            StoreManager.MediaStore = new MediaStore();
        }
    }
}
