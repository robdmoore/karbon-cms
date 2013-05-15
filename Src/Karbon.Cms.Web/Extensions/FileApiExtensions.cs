using System;
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

        public static IFilteredImage Fit(this IImageFile image, int size)
        {
            return new FilteredImage(image).Fit(size);
        }

        public static IFilteredImage Fit(this IFilteredImage image, int size)
        {
            image.Filters.Add("w", size);
            image.Filters.Add("h", size);
            image.Filters.Add("mode", "max");
            return image;
        }

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

    #endregion
}
