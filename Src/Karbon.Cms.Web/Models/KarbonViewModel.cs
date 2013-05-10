using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Models
{
    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    /// <typeparam name="TSiteType">The type of the site type.</typeparam>
    public class KarbonViewModel<TContentType, TSiteType>
        where TContentType : IContent
        where TSiteType : ISite
    {
        /// <summary>
        /// Gets the curret content.
        /// </summary>
        /// <value>
        /// The current content.
        /// </value>
        public TContentType Content { get; private set; }

        /// <summary>
        /// Gets the global site settings.
        /// </summary>
        /// <value>
        /// The global site settings.
        /// </value>
        public TSiteType Site { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel{TContentType, TSiteType}"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public KarbonViewModel(TContentType content,
            TSiteType site)
        {
            Content = content;
            Site = site;
        }
    }

    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    public class KarbonViewModel<TContentType> : KarbonViewModel<TContentType, Site>
        where TContentType : IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel{TContentType}"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public KarbonViewModel(TContentType content, Site site) 
            : base(content, site)
        { }
    }

    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
    public class KarbonViewModel : KarbonViewModel<Content, Site>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public KarbonViewModel(Content content, Site site) 
            : base(content, site)
        { }
    }
}
