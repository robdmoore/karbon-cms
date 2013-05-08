using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Mapping;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Serialization;

namespace Karbon.Cms.Core.Stores
{
    public class ContentStore : IContentStore
    {
        private FileStore _fileStore;
        private DataSerializer _dataSerializer;

        private IList<IContent> _contentCache = new List<IContent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStore"/> class.
        /// </summary>
        public ContentStore()
        {
            _fileStore = FileStoreManager.ContentFileStore;
            _dataSerializer = DataSerializerManager.Default;

            // Cache all the content at startup
            InitContentCache();
        }

        /// <summary>
        /// Initializes the content cache.
        /// </summary>
        private void InitContentCache()
        {
            CacheDirectoryRecursive("");
        }

        /// <summary>
        /// Caches the content in a directory recursivly.
        /// </summary>
        /// <param name="path">The path.</param>
        private void CacheDirectoryRecursive(string path)
        {
            var dirs = _fileStore.GetDirectories(path);
            foreach (var dir in dirs)
            {
                var content = GetByPath(dir);
                if (content != null)
                    _contentCache.Add(content);

                CacheDirectoryRecursive(dir);
            }
        }

        /// <summary>
        /// Gets a content item by URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public IContent GetByUrl(string url)
        {
            // Make sure URL isn't null
            if (url == null)
                return null;

            // TODO: Check the cache

            // Get content path from URL
            var contentPath = GetPathFromUrl(url);
            if(contentPath == null)
                return null;

            return GetByPath(contentPath);
        }

        /// <summary>
        /// Gets a content item by relative file path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private IContent GetByPath(string path)
        {
            if (path == null)
                return null;

            // TODO: Check the cache

            // Check directory exists
            if (!_fileStore.DirectoryExists(path))
                return null;

            // Parse directory name
            var directoryNameInfo = ParseName(_fileStore.GetName(path));

            // Grab first content file
            var contentFile = _fileStore.GetFiles(path)
                .FirstOrDefault();

            if (contentFile == null)
                return null;

            // Create model object based on file name
            var fileName = _fileStore.GetNameWithoutExtension(contentFile);
            var type = TypeFinder.FindTypes<Content>()
                           .SingleOrDefault(x => x.Name == fileName)
                       ?? typeof(Content);

            var model = Activator.CreateInstance(type) as IContent;
            if (model == null)
                return null;

            // Deserialize data
            var data = _dataSerializer.Deserialize(_fileStore.OpenFile(contentFile));

            // Map data to model
            model.Path = path;
            model.TypeName = fileName;
            model.Slug = directoryNameInfo.Name;
            model.Url = GetUrlFromPath(path);
            model.SortOrder = directoryNameInfo.SortOrder;
            model.Created = _fileStore.GetCreated(contentFile);
            model.Modified = _fileStore.GetLastModified(contentFile);

            model = (IContent)new DataMapper().Map(type, model, data);

            // Return model
            return model;
        }

        /// <summary>
        /// Gets a relative file path from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private string GetPathFromUrl(string url)
        {
            // Prepair URL
            url = url.ToLower().Trim('/');
            var urlParts = url.Split('/');

            // If a root request, get content from home 
            // TODO: Not sure if this should be part of this method or not?
            if (urlParts.Length == 1 && urlParts[0] == "")
                urlParts[0] = "home";

            var contentPath = "";

            // Loop URL parts
            foreach (var urlPart in urlParts)
            {
                var dirs = _fileStore.GetDirectories(contentPath).ToList();
                var possibleMatches = dirs.Where(x => x.ToLower().EndsWith(urlPart)).ToList();
                var match = possibleMatches.Count == 1
                    ? possibleMatches[0]
                    : possibleMatches.SingleOrDefault(x =>
                        Regex.IsMatch(x.ToLower(), @"^" + contentPath + @"[0-9]+\-" + urlPart));

                if (match != null)
                {
                    contentPath = match + "/";
                }
                else
                {
                    return null;
                }
            }

            // Return mapped path
            return contentPath.TrimEnd('/');
        }

        /// <summary>
        /// Gets the URL from a relative file path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string GetUrlFromPath(string path)
        {
            var pathParts = _fileStore.GetPathParts(path);

            var urlParts = pathParts
                .Select(ParseName)
                .Select(nameInfo => nameInfo.Name)
                .ToList();

            return string.Join("/", urlParts);
        }

        /// <summary>
        /// Parses a folder name into it's constituent parts.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private NameInfo ParseName(string name)
        {
            var fileNameInfo = new NameInfo { FullName = name };

            if (name.IndexOf('-') > 0)
            {
                var hyphenIndex = name.IndexOf('-');
                var possibleSortOrder = name.Substring(0, hyphenIndex);

                int parsedSortOrder;
                if(int.TryParse(possibleSortOrder, out parsedSortOrder))
                {
                    fileNameInfo.Name = name.Substring(hyphenIndex + 1);
                    fileNameInfo.SortOrder = parsedSortOrder;
                    return fileNameInfo;
                }
            }

            fileNameInfo.Name = name;
            fileNameInfo.SortOrder = -1;

            return fileNameInfo;
        }
    }
}
