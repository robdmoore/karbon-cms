using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Twitter", @"twitter\.com/")]
    public class TwitterEmbedProvider : AbstractRichEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "https://api.twitter.com/1/statuses/oembed.xml"; }
        }
    }
}
