using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private DataMapper _dataMapper;

        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        private IDictionary<string, IContent> _contentCache = new ConcurrentDictionary<string, IContent>();
        private bool _cacheDirty = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStore"/> class.
        /// </summary>
        public ContentStore()
        {
            // Setup required components
            _fileStore = FileStoreManager.Default;
            _dataSerializer = DataSerializerManager.Default;
            _dataMapper = new DataMapper();

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
            var url = content.RelativeUrl.EnsureTrailingForwardSlash();

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
                        var data = ParseContent();
                        _contentCache = new ConcurrentDictionary<string, IContent>(data);
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
            var directoryNameInfo = ParseDirectoryName(_fileStore.GetName(path));

            // Grab a files
            var filePaths = _fileStore.GetFiles(path).ToList();

            // Find the content file
            var contentFilePath = filePaths
                .FirstOrDefault(x => x.Count(y => y == '.') == 1 
                    && x.EndsWith("." + _dataSerializer.FileExtension));
            
            // Create model object based on file name
            var fileName = contentFilePath != null
                ? _fileStore.GetNameWithoutExtension(contentFilePath) 
                : "Content";

            var type = TypeFinder.FindTypes<Content>()
                           .SingleOrDefault(x => x.Name == fileName)
                       ?? typeof(Content);

            var model = Activator.CreateInstance(type) as IContent;
            if (model == null)
                return null;

            // Deserialize data
            var data = contentFilePath != null
                ? _dataSerializer.Deserialize(_fileStore.OpenFile(contentFilePath))
                : new Dictionary<string, string>();

            // Map data to model
            model.RelativePath = path;
            model.TypeName = fileName;
            model.Slug = directoryNameInfo.Name;
            model.RelativeUrl = GetUrlFromPath(path);
            model.SortOrder = directoryNameInfo.SortOrder;
            model.Created = _fileStore.GetCreated(contentFilePath ?? path);
            model.Modified = _fileStore.GetLastModified(contentFilePath ?? path);
            model.Depth = model.RelativeUrl == "~/" ? 1 : model.RelativeUrl.Count(x => x == '/') + 1;

            model = (IContent)_dataMapper.Map(type, model, data);

            // Parse files
            model.AllFiles = ParseFiles(filePaths.Where(x => x != contentFilePath), model.RelativeUrl);

            // Return model
            return model;
        }

        /// <summary>
        /// Parses the files from the file paths list provided.
        /// </summary>
        /// <param name="filePaths">The file paths.</param>
        /// <param name="contentUrl">The content relative URL.</param>
        /// <returns></returns>
        private IList<IFile> ParseFiles(IEnumerable<string> filePaths, string contentUrl)
        {
            var files = new List<IFile>();

            var noneContentFilePaths = filePaths.Where(x => !x.EndsWith("." + _dataSerializer.FileExtension));
            foreach (var noneContentFilePath in noneContentFilePaths)
            {
                // Parse file nae info
                var fileNameInfo = ParseFileName(_fileStore.GetName(noneContentFilePath));

                // See if there is a meta data file
                var contentFilePath =
                    filePaths.SingleOrDefault(x => x == noneContentFilePath + "." + _dataSerializer.FileExtension);

                // Find type for the file
                var type = TypeFinder.FindTypes<File>()
                           .SingleOrDefault(x => x.Name == fileNameInfo.TypeName)
                       ?? typeof(File);

                // Create the file
                var model = Activator.CreateInstance(type) as IFile;
                if (model == null)
                    continue;

                // Map data to the file
                model.RelativePath = noneContentFilePath;
                model.TypeName = fileNameInfo.TypeName;
                model.Slug = fileNameInfo.Name;
                model.RelativeUrl = "~/media/" + contentUrl.TrimStart("~/") + "/" + model.Slug;
                model.SortOrder = fileNameInfo.SortOrder;
                model.Extension = System.IO.Path.GetExtension(model.Slug);
                model.Created = _fileStore.GetCreated(noneContentFilePath);
                model.Modified = _fileStore.GetLastModified(noneContentFilePath);

                var data = contentFilePath != null
                    ? _dataSerializer.Deserialize(_fileStore.OpenFile(contentFilePath))
                    : new Dictionary<string, string>();

                // TODO: Make this bit provider driven so people 
                // can retreive their own data from a file
                if(model.IsImage())
                {
                    // TODO: Parse width height
                }

                model = (IFile)_dataMapper.Map(type, model, data);

                // Return the file
                files.Add(model);
            }

            return files;
        }

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
                .Select(ParseDirectoryName)
                .Select(nameInfo => nameInfo.Name)
                .ToList();

            return "~/" + string.Join("/", urlParts);
        }

        /// <summary>
        /// Parses a directory name into it's constituent parts.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private DirectoryNameInfo ParseDirectoryName(string name)
        {
            var dirNameInfo = new DirectoryNameInfo
            {
                FullName = name,
                Name = name,
                SortOrder = -1
            };

            if (name.IndexOf('-') > 0)
            {
                var hyphenIndex = name.IndexOf('-');
                var possibleSortOrder = name.Substring(0, hyphenIndex);

                int parsedSortOrder;
                if(int.TryParse(possibleSortOrder, out parsedSortOrder))
                {
                    dirNameInfo.Name = name.Substring(hyphenIndex + 1);
                    dirNameInfo.SortOrder = parsedSortOrder;
                    return dirNameInfo;
                }
            }

            return dirNameInfo;
        }

        /// <summary>
        /// Parses a file name into it's constituent parts.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private FileNameInfo ParseFileName(string name)
        {
            var fileNameInfo = new FileNameInfo
            {
                FullName = name,
                SortOrder = -1,
                TypeName = "File"
            };

            var ext = System.IO.Path.GetExtension(name);

            var nameParts = name.Split('.');
            if(nameParts.Length > 2)
            {
                fileNameInfo.TypeName = nameParts[1];
            }

            int parsedSortOrder;
            if (nameParts[0].IndexOf('-') > 0)
            {
                var hyphenIndex = nameParts[0].IndexOf('-');
                var possibleSortOrder = nameParts[0].Substring(0, hyphenIndex);

                if (int.TryParse(possibleSortOrder, out parsedSortOrder))
                {
                    fileNameInfo.Name = nameParts[0].Substring(hyphenIndex + 1) + ext;
                    fileNameInfo.SortOrder = parsedSortOrder;
                }
            }
            else
            {
                fileNameInfo.Name = nameParts[0] + ext;
                if(int.TryParse(nameParts[0], out parsedSortOrder))
                {
                    fileNameInfo.SortOrder = parsedSortOrder;
                }
            }
            
               
            return fileNameInfo;
        }

        #endregion
    }
}
