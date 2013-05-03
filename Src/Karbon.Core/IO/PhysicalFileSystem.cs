using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.IO
{
    public class PhysicalFileSystem : FileSystem
    {
        private string _virtualRoot;

        public override void Initialize(NameValueCollection config)
        {
            base.Initialize(config);

            _virtualRoot = config["virtualRoot"];
        }
    }
}
