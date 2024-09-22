using Microsoft.Extensions.DependencyInjection;
using System;

namespace Parsera
{
    /// <summary>
    /// ParseraClient service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ParseraClient to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="apiKey">The API key.</param>
        public static void AddParseraClient(this IServiceCollection services, string apiKey)
        {
            services.AddSingleton<IParseraClient>(new ParseraClient(apiKey));
        }

        /// <summary>
        /// Adds the ParseraClient to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="options">The options.</param>
        public static void AddParseraClient(this IServiceCollection services, ParseraClientOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddSingleton<IParseraClient>(new ParseraClient(options));
        }
    }
}
