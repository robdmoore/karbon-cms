using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Serialization;
using NUnit.Framework;

namespace Karbon.Cms.Tests.Serialization
{
    public abstract class AbstractDataSerializerTests
    {
        protected Stream CreateStream(string contents)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(contents));
        }
    }
}
