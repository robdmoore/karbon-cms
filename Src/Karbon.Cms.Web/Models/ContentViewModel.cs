using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Models
{
    /// <summary>
    /// The view model class for Karbon based content views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    /// <typeparam name="TSiteType">The type of the site type.</typeparam>
    public class ContentViewModel<TContentType, TSiteType>
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
        /// Initializes a new instance of the <see cref="ContentViewModel{TContentType, TSiteType}"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public ContentViewModel(TContentType content,
            TSiteType site)
        {
            Content = content;
            Site = site;
        }
    }

    /// <summary>
    /// The view model class for Karbon based content views.
    /// </summary>
    /// <typeparam name="TContentType">The type of the content type.</typeparam>
    public class ContentViewModel<TContentType> : ContentViewModel<TContentType, Site>
        where TContentType : IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel{TContentType}"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public ContentViewModel(TContentType content, Site site) 
            : base(content, site)
        { }
    }

    /// <summary>
    /// The view model class for Karbon based content views.
    /// </summary>
    public class ContentViewModel : ContentViewModel<Content, Site>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="site">The site.</param>
        public ContentViewModel(Content content, Site site) 
            : base(content, site)
        { }
    }
}
