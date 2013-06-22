using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Flickr", @"flickr\.com/")]
    public class FlickrEmbedProvider : AbstractPhotoEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://www.flickr.com/services/oembed/"; }
        }
    }
}
