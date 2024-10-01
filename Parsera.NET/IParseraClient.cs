using Parsera.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parsera
{
    /// <summary>
    /// Represents a client for the Parsera API.
    /// </summary>
    public interface IParseraClient
    {
        /// <summary>
        /// Extracts the content of a web page.
        /// </summary>
        /// <typeparam name="TExtractionModel">Extraction model template</typeparam>
        /// <param name="url">Url</param>
        /// <param name="proxyCountry">Proxy country</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>
        /// A collection of <typeparamref name="TExtractionModel"/> objects.
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task<IEnumerable<TExtractionModel>> ExtractAsync<TExtractionModel>(string url, string proxyCountry, CancellationToken cancellation = default);

        /// <summary>
        /// Parses the content of a web page.
        /// </summary>
        /// <typeparam name="TExtractionModel">Extraction model template</typeparam>
        /// <param name="content">Web page content (raw html or some other form of web content)</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>
        /// A collection of <typeparamref name="TExtractionModel"/> objects.
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task<IEnumerable<TExtractionModel>> ParseAsync<TExtractionModel>(string content, CancellationToken cancellation = default);

        /// <summary>
        /// Gets the available proxy countries.
        /// </summary>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>
        /// A collection of <see cref="ProxyCountry"/> objects.
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task<IEnumerable<ProxyCountry>> GetProxyCountriesAsync(CancellationToken cancellation = default);
    }
}
