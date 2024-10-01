using Microsoft.Extensions.Configuration;

namespace Parsera.Tests;

public class ParseTests
{
    private readonly IParseraClient _parseraClient;

    public ParseTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<ParseTests>();
        var configuration = builder.Build();

        _parseraClient = new ParseraClient(configuration["ParseraApiKey"] ?? string.Empty);
    }

    class ExtractionModel
    {
        [Extraction("Title", "News title")]
        public string Title { get; set; }

        [Extraction("Points", "Number of points")]
        public int Points { get; set; }
    }

    private async Task<string> GetHtmlContent(string url)
    {
        var httpClient = new HttpClient();
        var httpResponse = await httpClient.GetAsync(url);
        return await httpResponse.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task ParseData()
    {
        var htmlContent = await GetHtmlContent("https://news.ycombinator.com/");

        var result = await _parseraClient.ParseAsync<ExtractionModel>(htmlContent);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.All(x => !string.IsNullOrWhiteSpace(x.Title)));
    }
}
