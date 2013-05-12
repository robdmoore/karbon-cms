using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Karbon.Cms.Core.IO;
using Karbon.Cms.Core.Mapping;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Core.Serialization;
using Karbon.Cms.Core.Threading;

namespace Karbon.Cms.Core.Stores
{
    internal class ContentStore : IContentStore
    {
        private FileStore _fileStore;
        private DataSerializer _dataSerializer;

        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private IDictionary<string, IContent> _contentCache = new Dictionary<string, IContent>();
        private bool _cacheDirty = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStore"/> class.
        /// </summary>
        public ContentStore()
        {
            // Setup required components
            _fileStore = FileStoreManager.ContentFileStore;
            _dataSerializer = DataSerializerManager.Default;

            // Setup file store event listener
            _fileStore.FileChanged += (sender, args) => _cacheDirty = true;
        }

        #region Public API

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

            if (!_contentCache.ContainsKey(url))
                return null;

            return _contentCache[url];
        }

        /// <summary>
        /// Gets the parent content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public IContent GetParent(IContent content)
        {
            if (content.RelativeUrl.LastIndexOf("/", StringComparison.InvariantCulture) <= 2)
                return null;

            var parentUrl = content.RelativeUrl.Substring(0, content.RelativeUrl.LastIndexOf("/", StringComparison.InvariantCulture));
            if (!_contentCache.ContainsKey(parentUrl))
                return null;

            return _contentCache[parentUrl];
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public IEnumerable<IContent> GetChildren(IContent content)
        {
            var url = content.RelativeUrl + "/";
            if (url == "~//")
                url = "~/";

            var children = _contentCache.Keys
                .Where(x => x != url && x.StartsWith(url)
                    && x.TrimStart(url).IndexOf("/", StringComparison.InvariantCulture) == -1)
                .Select(x => _contentCache[x])
                .ToList();

            return children;
        }

        /// <summary>
        /// Gets the descendants.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public IEnumerable<IContent> GetDescendants(IContent content)
        {
            var url = content.RelativeUrl + "/";
            if (url == "~//")
                url = "~/";

            var descendants = _contentCache.Keys
                .Where(x => x != url && x.StartsWith(url))
                .Select(x => _contentCache[x])
                .ToList();

            return descendants;
        }

        #endregion

        #region Cache Control

        /// <summary>
        /// Syncs the content cache.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SyncCache()
        {
            if(_cacheDirty)
            {
                using (new WriteLock(_cacheLock))
                {
                    if(_cacheDirty)
                    {
                        _contentCache = ParseContent();
                        _cacheDirty = false;
                    }
                }
            }
        }

        /// <summary>
        /// Parses the content.
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, IContent> ParseContent()
        {
            return ParseContentDirectories(new[] {""}, new Dictionary<string, IContent>());
        }


        /// <summary>
        /// Parses the content directories.
        /// </summary>
        /// <param name="dirs">The dirs.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private IDictionary<string, IContent> ParseContentDirectories(IEnumerable<string> dirs, IDictionary<string, IContent> data)
        {
            foreach (var dir in dirs)
            {
                var content = GetByPath(dir);
                if (content != null)
                    data.Add(content.RelativeUrl, content);

                ParseContentDirectories(_fileStore.GetDirectories(dir), data);
            }

            return data;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets a content item by relative file path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private IContent GetByPath(string path)
        {
            if (path == null)
                return null;

            // Check directory exists
            if (!_fileStore.DirectoryExists(path))
                return null;

            // Parse directory name
            var directoryNameInfo = ParseName(_fileStore.GetName(path));

            // Grab first content file
            var contentFile = _fileStore.GetFiles(path)
                .FirstOrDefault();
            
            // Create model object based on file name
            var fileName = contentFile != null 
                ? _fileStore.GetNameWithoutExtension(contentFile) 
                : "Content";

            var type = TypeFinder.FindTypes<Content>()
                           .SingleOrDefault(x => x.Name == fileName)
                       ?? typeof(Content);

            var model = Activator.CreateInstance(type) as IContent;
            if (model == null)
                return null;

            // Deserialize data
            var data = contentFile != null 
                ? _dataSerializer.Deserialize(_fileStore.OpenFile(contentFile))
                : new Dictionary<string, string>();

            // Map data to model
            model.RelativePath = path;
            model.TypeName = fileName;
            model.Slug = directoryNameInfo.Name;
            model.RelativeUrl = GetUrlFromPath(path);
            model.SortOrder = directoryNameInfo.SortOrder;
            model.Created = _fileStore.GetCreated(contentFile ?? path);
            model.Modified = _fileStore.GetLastModified(contentFile ?? path);
            model.Depth = model.RelativeUrl == "~/" ? 1 : model.RelativeUrl.Count(x => x == '/') + 1;

            model = (IContent)new DataMapper().Map(type, model, data);

            // Return model
            return model;
        }

        ///// <summary>
        ///// Gets a relative file path from URL.
        ///// </summary>
        ///// <param name="url">The URL.</param>
        ///// <returns></returns>
        //private string GetPathFromUrl(string url)
        //{
        //    // Prepair URL
        //    url = url.ToLower().Trim('/');
        //    var urlParts = url.Split('/');

        //    // If a root request, get content from home 
        //    if (urlParts.Length == 1 && urlParts[0] == "")
        //        urlParts[0] = Constants.Home;

        //    var contentPath = "";

        //    // Loop URL parts
        //    foreach (var urlPart in urlParts)
        //    {
        //        var dirs = _fileStore.GetDirectories(contentPath).ToList();
        //        var possibleMatches = dirs.Where(x => x.ToLower().EndsWith(urlPart)).ToList();
        //        var match = possibleMatches.Count == 1
        //            ? possibleMatches[0]
        //            : possibleMatches.SingleOrDefault(x =>
        //                Regex.IsMatch(x.ToLower(), @"^" + contentPath + @"[0-9]+\-" + urlPart));

        //        if (match != null)
        //        {
        //            contentPath = match + "/";
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    // Return mapped path
        //    return contentPath.TrimEnd('/');
        //}

        /// <summary>
        /// Gets the URL from a relative file path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string GetUrlFromPath(string path)
        {
            if (path == null)
                return null;

            var pathParts = _fileStore.GetPathParts(path);
            var urlParts = pathParts
                .Select(ParseName)
                .Select(nameInfo => nameInfo.Name)
                .ToList();

            return "~/" + string.Join("/", urlParts);
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

        #endregion
    }
}
