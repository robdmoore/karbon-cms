using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Models
{
    public class ContentViewModel<TContentType, TSiteType>
        where TContentType : IContent
        where TSiteType : ISite
    {
        public TContentType Content { get; private set; }
        public TSiteType Site { get; private set; }

        public ContentViewModel(TContentType content,
            TSiteType site)
        {
            Content = content;
            Site = site;
        }
    }

    public class ContentViewModel<TContentType> : ContentViewModel<TContentType, Site>
        where TContentType : IContent
    {
        public ContentViewModel(TContentType content, Site site) 
            : base(content, site)
        { }
    }

    public class ContentViewModel : ContentViewModel<Content, Site>
    {
        public ContentViewModel(Content content, Site site) 
            : base(content, site)
        { }
    }
}
