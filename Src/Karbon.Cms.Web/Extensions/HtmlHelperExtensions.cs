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

        /// <summary>
        /// Converts new lines to HTML breaks.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IHtmlString Multiline(this HtmlHelper helper, string input)
        {
            return new HtmlString(input.Replace(Environment.NewLine, "<br />"));
        }

        /// <summary>
        /// Extracts a short excerpt from the input string.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="input">The input.</param>
        /// <param name="length">The length.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        public static IHtmlString Excerpt(this HtmlHelper helper, string input, int length = 150, string suffix = "...")
        {
            if(input == null)
                input = "";

            if (input.Length > length - suffix.Length)
                input = input.Substring(0, length - suffix.Length) + suffix;

            return new HtmlString(input);
        }
    }
}
