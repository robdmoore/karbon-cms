using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core
{
    public static class FileApiExtensions
    {
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

            switch (file.Extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".png":
                case ".bmp":
                case ".tif":
                case ".tiff":
                    return true;
                default:
                    return false;
            }
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

            switch (file.Extension.ToLower())
            {
                case ".ogg":
                case ".ogv":
                case ".webm":
                case ".mp4":
                case ".mov":
                case ".avi":
                case ".flv":
                case ".swf":
                    return true;
                default:
                    return false;
            }
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

            switch (file.Extension.ToLower())
            {
                case ".mp3":
                case ".wav":
                case ".wma":
                case ".mid":
                case ".ra":
                case ".ram":
                case ".rm":
                    return true;
                default:
                    return false;
            }
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

            switch (file.Extension.ToLower())
            {
                case ".pdf":
                case ".doc":
                case ".docx":
                case ".xls":
                case ".xlsx":
                case ".ppt":
                case ".pptx":
                case ".rtf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
