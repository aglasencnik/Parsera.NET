using Microsoft.Extensions.Configuration;
using Parsera.Models;

namespace Parsera.Tests;

public class ExtractionTests
{
    private readonly IParseraClient _parseraClient;

    public ExtractionTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _parseraClient = new ParseraClient(configuration["ParseraApiKey"] ?? string.Empty);
    }

    [Fact]
    public async Task ExtractData()
    {
        var extractionRequest = new ExtractionRequest
        {
            Url = "https://news.ycombinator.com/",
            Attributes =
            [
                new ExtractionAttribute
                {
                    Name = "Title",
                    Description = "News title"
                },
                new ExtractionAttribute
                {
                    Name = "Points",
                    Description = "Number of points"
                },
            ],
            ProxyCountry = "UnitedStates"
        };

        var result = await _parseraClient.ExtractAsync(extractionRequest);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data);
    }
}
