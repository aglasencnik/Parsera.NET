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
        /// Extracts data from a website asynchronously.
        /// </summary>
        /// <param name="extractionRequest">Extraction request</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>
        /// An <see cref="ExtractionResult"/> object.
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task<ExtractionResult> ExtractAsync(ExtractionRequest extractionRequest, CancellationToken cancellation = default);

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
