using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Karbon.Cms.Web
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this IEnumerable<KeyValuePair<string, object>> dict)
        {
            if (dict == null || !dict.Any())
                return "";

            return "?" + string.Join("&", dict.Select(x => string.Format("{0}={1}",
                HttpUtility.UrlEncode(x.Key),
                HttpUtility.UrlEncode(x.Value.ToString()))));
        }
    }
}
