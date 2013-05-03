using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core
{
    public class KarbonAppContext
    {
        #region Factory

        public static KarbonAppContext Current { get; internal set; }

        #endregion

        public EnvironmentContext Environment { get; set; }

        internal KarbonAppContext()
        {
            // Setup the default environment behaviour
            Environment = new EnvironmentContext();
        }
    }
}
