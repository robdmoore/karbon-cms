using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Web;
using Karbon.Web.Routing;

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