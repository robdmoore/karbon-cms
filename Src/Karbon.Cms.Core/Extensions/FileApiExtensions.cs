using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core
{
    public static class FileApiExtensions
    {
        #region Files

        /// <summary>
        /// Determines whether the specified file is an image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is an image; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsImage(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsImageExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a video.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a video; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsVideo(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsVideoExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a sound.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a sound; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSound(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsSoundExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Determines whether the specified file is a document.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the specified file is a document; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDocument(this IFile file)
        {
            if (file.Extension == null)
                return false;

            return IOHelper.IsDocumentExtension(file.Extension.ToLower());
        }

        /// <summary>
        /// Gets the mime type for the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static string MimeType(this IFile file)
        {
            return IOHelper.MimeTypes.ContainsKey(file.Extension.ToLower())
                ? IOHelper.MimeTypes[file.Extension.ToLower()]
                : "application/octet-stream";
        }

        #endregion
    }
}
