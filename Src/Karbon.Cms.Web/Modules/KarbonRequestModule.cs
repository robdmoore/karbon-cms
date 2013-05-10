using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Karbon.Cms.Web.Modules
{
    internal class KarbonRequestModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) =>
            {
                var test = 1;
            };
        }

        public void Dispose()
        { }
    }
}
