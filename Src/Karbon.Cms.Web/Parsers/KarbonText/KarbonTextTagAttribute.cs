using System;

namespace Karbon.Cms.Web.Parsers.KarbonText
{
    /// <summary>
    /// Used to decorate a Karbon Text tag parser
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KarbonTextTagAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonTextTagAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public KarbonTextTagAttribute(string name)
        {
            Name = name;
        }
    }
}
