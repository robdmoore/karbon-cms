using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.Embed
{
    [EmbedProvider("Vimeo", @"vimeo\.com/")]
    public class VimeoEmbedProvider : AbstractVideoEmbedProvider
    {
        /// <summary>
        /// Gets the API endpoint.
        /// </summary>
        /// <value>
        /// The API endpoint.
        /// </value>
        public override string ApiEndpoint
        {
            get { return "http://vimeo.com/api/oembed.xml"; }
        }
    }
}
