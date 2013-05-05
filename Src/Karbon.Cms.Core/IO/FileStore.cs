using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.IO
{
    public abstract class FileStore : ProviderBase
    {
        public override void Initialize(string name, 
            NameValueCollection config)
        {
            base.Initialize(name, config);
            Initialize(config);
        }

        public virtual void Initialize(NameValueCollection config) { }

        #region Directories

        public abstract IEnumerable<string> GetDirectories(string relativePath = "");
        public abstract void DeleteDirectory(string relativePath, bool recursive = false);
        public abstract bool DirectoryExists(string relativePath);

        #endregion

        #region Files

        public abstract IEnumerable<string> GetFiles(string relativePath = "", string filter = "*.*");
        public abstract void AddFile(string relativePath, Stream stream, bool overwrite = true);
        public abstract Stream OpenFile(string relativePath);
        public abstract void DeleteFile(string relativePath);
        public abstract bool FileExists(string relativePath);

        #endregion

        #region Helper Methods

        public abstract DateTimeOffset GetLastModified(string relativePath);
        public abstract DateTimeOffset GetCreated(string relativePath);
        public abstract DateTimeOffset GetLastAccessed(string relativePath);
        public abstract string GetAbsolutePath(string relativePath);

        #endregion
    }
}
