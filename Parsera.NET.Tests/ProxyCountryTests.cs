using Microsoft.Extensions.Configuration;

namespace Parsera.Tests;

public class ProxyCountryTests
{
    private readonly IParseraClient _parseraClient;

    public ProxyCountryTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<ProxyCountryTests>();
        var configuration = builder.Build();

        _parseraClient = new ParseraClient(configuration["ParseraApiKey"] ?? string.Empty);
    }

    [Fact]
    public async Task GetProxyCountries()
    {
        var countries = await _parseraClient.GetProxyCountriesAsync();

        Assert.NotNull(countries);
        Assert.NotEmpty(countries);
    }
}
