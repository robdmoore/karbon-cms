using System.Collections.Generic;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    public abstract class AbstractKarbonTextTag
    {
        public abstract string GetMarkup(IContent currentPage, IDictionary<string, string> parameters);
    }
}
