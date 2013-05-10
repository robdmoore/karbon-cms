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
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    /// <typeparam name="TSiteType">The type of the site type.</typeparam>
    public abstract class KarbonView<TContentType, TSiteType> : WebViewPage<KarbonViewModel<TContentType, TSiteType>>
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
                viewData.Model = new KarbonViewModel<TContentType, TSiteType>((TContentType) viewData.Model,
                    default(TSiteType)); // TODO: Populate site model
            }

            base.SetViewData(viewData);
        }
    }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content.</typeparam>
    public abstract class KarbonView<TContentType> : KarbonView<TContentType, Site>
        where TContentType : IContent
    { }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    public abstract class KarbonView : KarbonView<Content, Site>
    { }
}
