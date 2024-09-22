using System;
using System.Net.Http;

namespace Parsera
{
    /// <inheritdoc />
    public class ParseraClient : IParseraClient
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        #endregion

        #region Constructors

        public ParseraClient(string apiKey)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.parsera.org/v1"),
            };

            _apiKey = apiKey;
        }

        public ParseraClient(ParseraClientOptions options) : this(options.ApiKey) { }

        #endregion
    }
}
