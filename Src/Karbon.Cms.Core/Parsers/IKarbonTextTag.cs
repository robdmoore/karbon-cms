using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Parsers
{
    internal interface IKarbonTextTag
    {
        string Parse(IContent currentPage, IDictionary<string, string> parameters);
    }
}
