using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type has a given method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has the given method; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasMethod(this Type type, string methodName)
        {
            return type.HasMethod(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, methodName);
        }

        /// <summary>
        /// Determines whether the specified type has a given method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has the given method; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasMethod(this Type type, BindingFlags bindingFlags, string methodName)
        {
            return type.GetMethods(bindingFlags)
                .Any(x => x.Name.ToLower() == methodName.ToLower());
        }

        /// <summary>
        /// Determines whether =is assignable from extended the specified type.
        /// </summary>
        /// <param name="extendType">Type of the extend.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns>
        ///   <c>true</c> if is assignable from the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAssignableFromExtended(this Type extendType, Type baseType)
        {
            while (!baseType.IsAssignableFrom(extendType))
            {
                if (extendType.Equals(typeof(object)))
                {
                    return false;
                }
                if (extendType.IsGenericType && !extendType.IsGenericTypeDefinition)
                {
                    extendType = extendType.GetGenericTypeDefinition();
                }
                else
                {
                    extendType = extendType.BaseType;
                }
            }
            return true;
        }
    }
}
