using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Core.IO;
using Karbon.Core.Serialization;
using Karbon.Core.Stores;

namespace Karbon.Core
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
