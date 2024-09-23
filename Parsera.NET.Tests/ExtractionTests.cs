using Microsoft.Extensions.Configuration;

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

    class ExtractionModel
    {
        [Extraction("Title", "News title")]
        public string Title { get; set; }

        [Extraction("Points", "Number of points")]
        public int Points { get; set; }
    }

    [Fact]
    public async Task ExtractData()
    {
        var result = await _parseraClient.ExtractAsync<ExtractionModel>("https://news.ycombinator.com/", "UnitedStates");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.All(x => !string.IsNullOrWhiteSpace(x.Title)));
    }
}
