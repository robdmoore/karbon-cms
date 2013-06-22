using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Parsers
{
    internal class KarbonTextParser
    {
        private IContent _currentPage;

        private IDictionary<string, Type> _tags; 
        private Regex _tagPattern = new Regex(@"\[(?:\s*)?(?<name>[a-zA-Z0-9]+)(?:\s*)?:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonTextParser"/> class.
        /// </summary>
        public KarbonTextParser(IContent currentPage)
        {
            _currentPage = currentPage;

            _tags = TypeFinder.FindTypes<IKarbonTextTag>()
                .Where(x => x.GetCustomAttribute<KarbonTextTagAttribute>() != null)
                .ToDictionary(x => x.GetCustomAttribute<KarbonTextTagAttribute>().Name, x => x);
        }

        /// <summary>
        /// Parses the tags.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public string ParseTags(string input)
        {
            return _tagPattern.Replace(input, match =>
            {
                var tagName = match.Groups["name"].Value.ToLower(CultureInfo.InvariantCulture);
                if (_tags.ContainsKey(tagName))
                {
                    var parameters = match.Value.TrimStart('[').TrimEnd(']')
                        .Split('|').ToDictionary(x => x.Substring(0, x.FindIndex(':')).Trim().ToLower(CultureInfo.InvariantCulture),
                                                 x => x.Substring(x.FindIndex(':') + 1).Trim());

                    var tag = Activator.CreateInstance(_tags[tagName]) as IKarbonTextTag;
                    return tag != null
                        ? tag.Parse(_currentPage, parameters)
                        : match.Value;
                }
                return match.Value;
            });
        }
    }
}
