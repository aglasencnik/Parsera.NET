using Parsera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
                BaseAddress = new Uri("https://api.parsera.org"),
            };

            _apiKey = apiKey;
        }

        public ParseraClient(ParseraClientOptions options) : this(options.ApiKey) { }

        #endregion

        #region Utils

        private async Task<string> GetResultAsStringAsync<TRequest>(string uri, HttpMethod httpMethod, TRequest requestModel, CancellationToken cancellation)
        {
            try
            {
                using (var request = new HttpRequestMessage(httpMethod, uri))
                {
                    request.Headers.Add("X-API-KEY", _apiKey);

                    if (requestModel != null)
                    {
                        var json = JsonSerializer.Serialize(requestModel);
                        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    }

                    using (var response = await _httpClient.SendAsync(request, cancellation))
                    {
                        if (!response.IsSuccessStatusCode)
                            return default;

                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch
            {
                return default;
            }
        }

        private async Task<TResponse> GetResultAsync<TResponse, TRequest>(string uri, HttpMethod httpMethod, TRequest requestModel, CancellationToken cancellation)
        {
            var result = await GetResultAsStringAsync(uri, httpMethod, requestModel, cancellation);

            try
            {
                return result == null ? default : JsonSerializer.Deserialize<TResponse>(result);
            }
            catch
            {
                return default;
            }
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<ExtractionResult> ExtractAsync(ExtractionRequest extractionRequest, CancellationToken cancellation = default)
        {
            var result = await GetResultAsync<IEnumerable<IDictionary<string, string>>, ExtractionRequest>(
                "/v1/extract", HttpMethod.Post, extractionRequest, cancellation);

            if (result == null)
                return null;

            return new ExtractionResult
            {
                Data = result ?? Enumerable.Empty<IDictionary<string, string>>(),
            };
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProxyCountry>> GetProxyCountriesAsync(CancellationToken cancellation = default)
        {
            var result = await GetResultAsync<IDictionary<string, string>, object>(
                "/v1/proxy-countries", HttpMethod.Get, null, cancellation);

            return result == null ? Enumerable.Empty<ProxyCountry>() : result.Select(x => new ProxyCountry
            {
                FriendlyName = x.Value,
                SystemName = x.Key,
            });
        }

        #endregion
    }
}
