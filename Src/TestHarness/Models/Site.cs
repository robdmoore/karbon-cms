using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karbon.Web.Model;

namespace TestHarness.Models
{
    public class Site : SiteModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}