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
        private bool _appStartingFlag;
        private bool _appStartedFlag;

        /// <summary>
        /// Initializes components that need to run before the application has started
        /// </summary>
        public virtual void AppStarting()
        {
            if (_appStartingFlag)
                throw new InvalidOperationException("The boot manager has already started");

            _appStartingFlag = true;
        }

        /// <summary>
        /// Initializes components that need to run after the application has started
        /// </summary>
        public virtual void AppStarted()
        {
            if (_appStartedFlag)
                throw new InvalidOperationException("The boot manager has already started");

            KarbonAppContext.Current = new KarbonAppContext();

            StoreManager.ContentStore = new ContentStore();
            StoreManager.MediaStore = new MediaStore();

            _appStartedFlag = true;
        }
    }
}
