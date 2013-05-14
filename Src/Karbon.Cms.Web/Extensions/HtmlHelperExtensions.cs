using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;

namespace Karbon.Cms.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Converts markdown content to HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string input)
        {
            return new HtmlString(new Markdown().Transform(input));
        }

        /// <summary>
        /// Converts markdown content to HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Md(this HtmlHelper helper, string input)
        {
            return helper.Markdown(input);
        }
    }
}
