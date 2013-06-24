using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("SoundCloud", @"soundcloud\.com/")]
    public class SoundCloudEmbedProvider : AbstractRichOEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://soundcloud.com/oembed"; }
        }
    }
}
