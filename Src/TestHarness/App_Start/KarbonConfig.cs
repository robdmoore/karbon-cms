using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Karbon.Cms.Web;
using Karbon.Cms.Web.Routing;
using TestHarness.App_Start;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(KarbonConfig), "Initialize")]

namespace TestHarness.App_Start
{
    public class KarbonConfig
    {
        public static void Initialize()
        {
            new WebBootManager()
                .Initialize();
        }
    }
}