using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karbon.Cms.Web.OEmbed
{
    [OEmbedProvider("YouTube", @"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)")]
    public class YouTubeOEmbedProvider : AbstractVideoOEmbedProvider
    {
        public override string ApiEndpoint
        {
            get { return "http://www.youtube.com/oembed"; }
        }

        public override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"iframe", "1"},
                    {"format", "xml"}
                };
            }
        }
    }
}
