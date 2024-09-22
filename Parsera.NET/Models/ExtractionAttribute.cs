using System.Text.Json.Serialization;

namespace Parsera.Models
{
    /// <summary>
    /// Represents an extraction attribute.
    /// </summary>
    public class ExtractionAttribute
    {
        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the attribute.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
