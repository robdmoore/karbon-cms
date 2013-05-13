using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Stores;

namespace Karbon.Cms.Core
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
            return content.Children(x => x.GetType() == typeof(TContentType))
                .Cast<TContentType>();
        }

        /// <summary>
        /// Gets the child content filtered by the provided filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Children(this IContent content, Func<IContent, bool> filter)
        {
            return content.Children().Where(filter);
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
            return content.Children<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content filtered by the supplied filter function.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Find(this IContent content, Func<IContent, bool> filter)
        {
            return StoreManager.ContentStore.GetDescendants(content)
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content of the given type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Find<TContentType>(this IContent content)
        {
            return content.Find(x => x.GetType() == typeof(TContentType)).Cast<TContentType>();
        }

        /// <summary>
        /// Gets the sibling content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Siblings(this IContent content)
        {
            if (content.IsHomePage())
                return Enumerable.Empty<IContent>();

            return content.Parent().Children(x => x.RelativeUrl != content.RelativeUrl);
        }

        /// <summary>
        /// Gets the sibling content filtered by the supplied filter function
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<IContent> Siblings(this IContent content, Func<IContent, bool> filter)
        {
            return content.Siblings().Where(filter);
        }

        /// <summary>
        /// Gets the sibling content of the given type.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Siblings<TContentType>(this IContent content)
        {
            return content.Siblings(x => x.GetType() == typeof (TContentType))
                .Cast<TContentType>();
        }

        /// <summary>
        /// Gets the sibling content of the given type filtered by the supplied filter function.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Siblings<TContentType>(this IContent content, Func<TContentType, bool> filter)
        {
            return content.Siblings<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Finds descendant content of the given type filtered by the supplied filter function.
        /// </summary>
        /// <typeparam name="TContentType">The type of the content type.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<TContentType> Find<TContentType>(this IContent content, Func<TContentType, bool> filter)
        {
            return content.Find<TContentType>()
                .Where(filter);
        }

        /// <summary>
        /// Determines whether the specified content is visible.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is visible; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsVisible(this IContent content)
        {
            return content.SortOrder >= 0;
        }

        /// <summary>
        /// Determines whether the specified content is the home page.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   <c>true</c> if the specified content is the home page; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHomePage(this IContent content)
        {
            return content.Depth == 1;
        }
    }
}
