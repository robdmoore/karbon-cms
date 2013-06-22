using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.OEmbed
{
    [OEmbedProvider("Vimeo", @"vimeo\.com/")]
    public class VimeoOEmbedProvider : AbstractVideoOEmbedProvider
    {
        public override string ApiEndpoint
        {
            get { return "http://vimeo.com/api/oembed.xml"; }
        }
    }
}
