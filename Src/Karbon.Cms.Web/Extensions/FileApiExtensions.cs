﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    public static class FileApiExtensions
    {
        #region Files
        
        #endregion

        #region Images

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <returns></returns>
        public static IFilteredImage FitWidth(this IImageFile image, int maxWidth)
        {
            return new FilteredImage(image).FitWidth(maxWidth);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <returns></returns>
        public static IFilteredImage FitWidth(this IFilteredImage image, int maxWidth)
        {
            image.Filters.Add("w", maxWidth);
            return image;
        }

        /// <summary>
        /// Fits the specified image to the supplied max height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxHeight">Max height.</param>
        /// <returns></returns>
        public static IFilteredImage FitHeight(this IImageFile image, int maxHeight)
        {
            return new FilteredImage(image).FitHeight(maxHeight);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxHeight">Max height.</param>
        /// <returns></returns>
        public static IFilteredImage FitHeight(this IFilteredImage image, int maxHeight)
        {
            image.Filters.Add("h", maxHeight);
            return image;
        }

        /// <summary>
        /// Fits the specified image to the supplied max width / height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidthHeight">max width / height.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IImageFile image, int maxWidthHeight, FitMode fitMode = FitMode.Pad)
        {
            return image.Fit(maxWidthHeight, maxWidthHeight, fitMode);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width / height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidthHeight">max width / height.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IFilteredImage image, int maxWidthHeight, FitMode fitMode = FitMode.Pad)
        {
            return image.Fit(maxWidthHeight, maxWidthHeight, fitMode);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width and height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IImageFile image, int maxWidth, int maxHeight, FitMode fitMode = FitMode.Pad)
        {
            return new FilteredImage(image).Fit(maxWidth, maxHeight, fitMode);
        }

        /// <summary>
        /// Fits the specified image to the supplied max width and height.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <param name="fitMode">The fit mode.</param>
        /// <returns></returns>
        public static IFilteredImage Fit(this IFilteredImage image, int maxWidth, int maxHeight, FitMode fitMode = FitMode.Pad)
        {
            image.Filters.Add("w", maxWidth);
            image.Filters.Add("h", maxHeight);

            if(fitMode != FitMode.Pad)
                image.Filters.Add("mode", fitMode.ToString().ToLower());

            return image;
        }

        /// <summary>
        /// Gets the absolute url for the given image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public static string Url(this IFilteredImage image)
        {
            return image.Image.Url() + image.Filters.ToQueryString();
        }

        #endregion

        #region Videos

        #endregion

        #region Sounds

        #endregion

        #region Documents

        #endregion
    }

    #region Helper Classes

    public interface IFilteredImage
    {
        IImageFile Image { get; set; }
        IDictionary<string, object> Filters { get; set; }
    }

    public class FilteredImage : IFilteredImage
    {
        public IImageFile Image { get; set; }
        public IDictionary<string, object> Filters { get; set; }

        public FilteredImage(IImageFile image)
        {
            Image = image;
            Filters = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this.Url();
        }
    }

    public enum FitMode
    {
        Pad,
        Max,
        Crop,
        Stretch,
        Carve
    }

    #endregion
}
