using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Web.Models;

namespace Karbon.Cms.Web.Mvc
{
    public abstract class ContentView<TContentType, TSiteType> : WebViewPage<ContentViewModel<TContentType, TSiteType>>
        where TContentType : IContent
        where TSiteType : ISite
    {
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if(viewData.Model is TContentType)
            {
                viewData.Model = new ContentViewModel<TContentType, TSiteType>((TContentType) viewData.Model,
                    default(TSiteType)); // TODO: Populate site model
            }

            base.SetViewData(viewData);
        }
    }

    public abstract class ContentView<TContentType> : ContentView<TContentType, Site>
        where TContentType : IContent
    { }

    public abstract class ContentView : ContentView<Content, Site>
    { }
}
