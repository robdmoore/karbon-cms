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
    /// <summary>
    /// The base class for Karbon based content views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    /// <typeparam name="TSiteType">The type of the site type.</typeparam>
    public abstract class ContentView<TContentType, TSiteType> : WebViewPage<ContentViewModel<TContentType, TSiteType>>
        where TContentType : IContent
        where TSiteType : ISite
    {
        /// <summary>
        /// Sets the view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
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

    /// <summary>
    /// The base class for Karbon based content views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    public abstract class ContentView<TContentType> : ContentView<TContentType, Site>
        where TContentType : IContent
    { }

    /// <summary>
    /// The base class for Karbon based content views.
    /// </summary>
    public abstract class ContentView : ContentView<Content, Site>
    { }
}
