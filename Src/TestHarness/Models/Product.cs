using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karbon.Cms.Core.Models;

namespace TestHarness.Models
{
    public class Product : Content
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}