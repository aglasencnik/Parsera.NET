using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Parsera.Tests;

public class ClientInstancingTests
{
    private string _apiKey;

    public ClientInstancingTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _apiKey = configuration["ParseraApiKey"] ?? string.Empty;
    }

    [Fact]
    public void CreateClientWithApiKey()
    {
        var parseraClient = new ParseraClient(_apiKey);
        Assert.NotNull(parseraClient);
    }

    [Fact]
    public void CreateClientWithOptions()
    {
        var options = new ParseraClientOptions
        {
            ApiKey = _apiKey
        };

        var parseraClient = new ParseraClient(options);

        Assert.NotNull(parseraClient);
    }

    [Fact]
    public async Task CreateClientWithDependencyInjectionWithApiKey()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddParseraClient(_apiKey);

        var serviceProvider = builder.Build().Services;

        var parseraClient = serviceProvider.GetRequiredService<IParseraClient>();

        Assert.NotNull(parseraClient);
    }

    [Fact]
    public async Task CreateClientWithDependencyInjectionWithOptions()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddParseraClient(new ParseraClientOptions
        {
            ApiKey = _apiKey
        });

        var serviceProvider = builder.Build().Services;

        var parseraClient = serviceProvider.GetRequiredService<IParseraClient>();

        Assert.NotNull(parseraClient);
    }
}
