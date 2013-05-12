using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;
using Karbon.Cms.Web.Models;

namespace Karbon.Cms.Web.Mvc
{
    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content.</typeparam>
    /// <typeparam name="THomePageContentType">The type of the home page content.</typeparam>
    public abstract class KarbonView<TCurrentPageContentType, THomePageContentType> : WebViewPage<KarbonViewModel<TCurrentPageContentType, THomePageContentType>>
        where TCurrentPageContentType : IContent
        where THomePageContentType : IContent
    {
        /// <summary>
        /// Sets the view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if (viewData.Model is TCurrentPageContentType)
            {
                viewData.Model = new KarbonViewModel<TCurrentPageContentType, THomePageContentType>((TCurrentPageContentType)viewData.Model,
                    default(THomePageContentType));
            }

            base.SetViewData(viewData);
        }
    }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content.</typeparam>
    public abstract class KarbonView<TCurrentPageContentType> : KarbonView<TCurrentPageContentType, Content>
        where TCurrentPageContentType : IContent
    { }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    public abstract class KarbonView : KarbonView<Content, Content>
    { }
}
