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

        private IEnumerable<ExtractionAttributeModel> GetExtractionAttributes<TExtractionModel>()
        {
            var attributes = new List<ExtractionAttributeModel>();

            foreach (var property in typeof(TExtractionModel).GetProperties())
            {
                var extractionAttribute = property.GetCustomAttributes(typeof(ExtractionAttribute), false).FirstOrDefault() as ExtractionAttribute;
                if (extractionAttribute == null)
                    continue;

                attributes.Add(new ExtractionAttributeModel
                {
                    Name = extractionAttribute.Name,
                    Description = extractionAttribute.Description,
                });
            }

            return attributes;
        }

        private IEnumerable<TExtractionModel> GetExtractionResults<TExtractionModel>(IEnumerable<IDictionary<string, string>> extractionResults)
        {
            if (extractionResults == null)
                return null;

            var extractionModels = new List<TExtractionModel>();

            foreach (var extractionResult in extractionResults)
            {
                var extractionModel = Activator.CreateInstance<TExtractionModel>();

                foreach (var property in typeof(TExtractionModel).GetProperties())
                {
                    var extractionAttribute = property.GetCustomAttributes(typeof(ExtractionAttribute), false).FirstOrDefault() as ExtractionAttribute;
                    if (extractionAttribute == null)
                        continue;

                    var value = extractionResult.FirstOrDefault(x => x.Key == extractionAttribute.Name).Value;
                    if (value == null)
                        continue;

                    property.SetValue(extractionModel, Convert.ChangeType(value, property.PropertyType));
                }

                extractionModels.Add(extractionModel);
            }

            return extractionModels;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<IEnumerable<TExtractionModel>> ExtractAsync<TExtractionModel>(string url, string proxyCountry, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(proxyCountry))
                return null;

            var extractionRequest = new ExtractionRequest
            {
                Url = url,
                Attributes = GetExtractionAttributes<TExtractionModel>(),
                ProxyCountry = proxyCountry,
            };

            var result = await GetResultAsync<IEnumerable<IDictionary<string, string>>, ExtractionRequest>(
                "/v1/extract", HttpMethod.Post, extractionRequest, cancellation);

            return GetExtractionResults<TExtractionModel>(result);
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
