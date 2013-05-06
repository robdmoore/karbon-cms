using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Karbon.Cms.Core.Threading;

namespace Karbon.Cms.Core
{
    public class TypeFinder
    {
        private static readonly string[] ExcludedAssemblies = new[]
        {
            "System.",
            "Microsoft.",
            "Newtonsoft.",
            "Karbon."
        };

        private static ReadOnlyCollection<Assembly> _assemblies = null;
        private static readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        private static IEnumerable<Assembly> GetAssemblies()
        {
            if (_assemblies == null)
            {
                using (new WriteLock(_locker))
                {
                    var binFolder = Assembly.GetExecutingAssembly().GetAssemblyFileInfo().Directory;
                    if(binFolder != null)
                    {
                        var assemblyFiles = Directory.GetFiles(binFolder.FullName, "*.dll", SearchOption.TopDirectoryOnly);
                        var assemblies = assemblyFiles
                            .Where(x => ExcludedAssemblies.All(y => !x.StartsWith(y)))
                            .Select(Assembly.LoadFrom)
                            .ToList();

                        _assemblies = new ReadOnlyCollection<Assembly>(assemblies);
                    }
                }
            }

            return _assemblies;
        }

        public static IEnumerable<Type> FindTypes<TType>(bool concreteOnly = true)
        {
            var tType = typeof(TType);

            return GetAssemblies()
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => !t.IsInterface && tType.IsAssignableFrom(t)
                    && (!concreteOnly || (t.IsClass && !t.IsAbstract)));
        }
    }
}
