using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Core.Serialization
{
    public abstract class DataSerializer : ProviderBase
    {
        public string FileExtension { get; private set; }   

        public override void Initialize(string name,
            NameValueCollection config)
        {
            base.Initialize(name, config);

            FileExtension = config["fileExtension"];

            Initialize(config);
        }

        public virtual void Initialize(NameValueCollection config) { }

        public abstract IDictionary<string, string> Deserialize(string pageData);
    }
}
