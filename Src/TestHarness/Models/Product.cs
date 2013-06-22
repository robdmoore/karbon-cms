using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Karbon.Cms.Core.Models;
using TestHarness.Controllers;

namespace TestHarness.Models
{
    public class Product : Content
    {
        public string Description { get; set; }
    }
}