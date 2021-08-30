using System;

namespace P41.AndroidViewGenerator
{
    /// <summary>
    /// Attribute used by the generator to generate the View.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GenerateViewAttribute : Attribute
    {
        /// <summary>
        /// The xml file to generate a View from.
        /// </summary>
        public string XmlFile { get; }

        // This is a positional argument
        /// <summary>
        /// Initialize a new <see cref="GenerateViewAttribute"/> instance.
        /// </summary>
        /// <param name="xmlFile">The file to generate a View from.</param>
        public GenerateViewAttribute(string xmlFile)
        {
            XmlFile = xmlFile;
        }
    }
}