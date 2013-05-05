using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Web;
using Karbon.Cms.Web.Routing;

namespace TestHarness.App_Start
{
    public class KarbonConfig
    {
        public static void Init()
        {
            new WebBootManager()
                .Initialize();
        }
    }
}