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
        private bool _appInitedFlag;

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public virtual void Initialize()
        {
            if (_appInitedFlag)
                throw new InvalidOperationException("The boot manager has already been initialized");

            KarbonAppContext.Current = new KarbonAppContext();

            StoreManager.ContentStore = new ContentStore();

            _appInitedFlag = true;
        }
    }
}
