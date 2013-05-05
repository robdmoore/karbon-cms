using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Karbon.Cms.Core;

namespace Karbon.Cms.Web
{
    public class WebEnvironmentContext : EnvironmentContext
    {
        private readonly HttpContextWrapper _httpContext;

        private string _rootDir;
        public override string RootDirectory
        {
            get
            {
                return _rootDir ?? (_rootDir = MapPath("~/"));
            }
        }

        public WebEnvironmentContext(HttpContextWrapper httpContext)
        {
            _httpContext = httpContext;
        }

        public override string MapPath(string path)
        {
            return _httpContext.Server.MapPath(path);
        }
    }
}
