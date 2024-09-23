using System;

namespace Parsera
{
    /// <summary>
    /// Represents an attribute that is used to mark a property as an extraction.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExtractionAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the extraction.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the extraction.
        /// </summary>
        public string Description { get; }

        public ExtractionAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
