using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parsera.Models
{
    /// <summary>
    /// Represents a request to extract data from a website.
    /// </summary>
    public class ExtractionRequest
    {
        /// <summary>
        /// Gets or sets the URL of the website to extract data from.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the attributes to extract from the website.
        /// </summary>
        [JsonPropertyName("attributes")]
        public IEnumerable<ExtractionAttribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the country of the proxy to use for the extraction.
        /// </summary>
        [JsonPropertyName("proxy_country")]
        public string ProxyCountry { get; set; } = "random";
    }
}
