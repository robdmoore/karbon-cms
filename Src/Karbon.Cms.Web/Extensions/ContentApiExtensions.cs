using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Web
{
    public static class ContentApiExtensions
    {
        /// <summary>
        /// Gets the parent content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IContent Parent(this IContent content)
        {
            return StoreManager.ContentStore.GetParent(content);
        }

        /// <summary>
        /// Gets the parent content cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static TContentType Parent<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return (TContentType)content.Parent();
        }

        /// <summary>
        /// Gets the child content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Children(this IContent content)
        {
            return StoreManager.ContentStore.GetChildren(content);
        }

        /// <summary>
        /// Gets the child content cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Children<TContentType>(this IContent content)
            where TContentType : IContent
        {
            return content.Children().Cast<TContentType>();
        }

        /// <summary>
        /// Gets the child content filtered by the provided filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Children(this IContent content, Func<IContent, bool> filter)
        {
            return StoreManager.ContentStore.GetChildren(content).Where(filter);
        }

        /// <summary>
        /// Gets the child content filtered by the provided filter function, cast to the specified type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Children<TContentType>(this IContent content, Func<TContentType, bool> filter)
            where TContentType : IContent
        {
            return content.Children().Cast<TContentType>().Where(filter);
        }
    }
}
