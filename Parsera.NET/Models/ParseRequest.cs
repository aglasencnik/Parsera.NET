using Parsera.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parsera
{
    /// <summary>
    /// Represents a request to parse a document.
    /// </summary>
    internal class ParseRequest
    {
        /// <summary>
        /// Gets or sets the content to parse.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the attributes to extract from the website.
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<ExtractionAttributeModel> Attributes { get; set; }
    }
}
