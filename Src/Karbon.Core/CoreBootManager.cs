using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core
{
    public class CoreBootManager : IBootManager
    {
        private bool _isInitialized;

        public virtual IBootManager Initialize()
        {
            if (_isInitialized)
                throw new InvalidOperationException("The boot manager has already been initialized");

            KarbonAppContext.Current = new KarbonAppContext();

            return this;
        }
    }
}
