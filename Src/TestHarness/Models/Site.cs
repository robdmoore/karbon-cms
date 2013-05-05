using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHarness.Models
{
    public class Site : Karbon.Cms.Core.Models.Site
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}