using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("SlideShare", @"slideshare\.net/")]
    public class SlideShareEmbedProvider : AbstractRichEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://www.slideshare.net/api/oembed/1"; }
        }

        /// <summary>
        /// Gets the default parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "format", "xml" }
                };
            }
        }
    }
}
