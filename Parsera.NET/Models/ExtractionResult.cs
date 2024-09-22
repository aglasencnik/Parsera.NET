using System.Collections.Generic;

namespace Parsera.Models
{
    /// <summary>
    /// Represents the result of an extraction.
    /// </summary>
    public class ExtractionResult
    {
        /// <summary>
        /// Represents the data extracted.
        /// </summary>
        public IEnumerable<IDictionary<string, string>> Data { get; set; }
    }
}
