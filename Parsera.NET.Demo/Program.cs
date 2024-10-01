using Microsoft.Extensions.Configuration;
using Parsera;

// Add your API key to the User Secrets
var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();
var configuration = builder.Build();
var apiKey = configuration["ParseraApiKey"] ?? string.Empty;

var parseraClient = new ParseraClient(apiKey);

// Display first 10 proxy countries
var proxyCountries = await parseraClient.GetProxyCountriesAsync();

Console.WriteLine("Proxy countries:");
foreach (var country in proxyCountries.Take(10))
{
    Console.WriteLine($"{country.FriendlyName} ({country.SystemName})");
}

// Extract data from Hacker News
var extractionResults = await parseraClient.ExtractAsync<ExtractionModel>("https://news.ycombinator.com/", "UnitedStates");

Console.WriteLine("\nExtraction results:");
var counter = 1;
foreach (var extraction in extractionResults)
{
    Console.WriteLine($"Extraction {counter++}");
    Console.WriteLine($"Title: {extraction.Title}");
    Console.WriteLine($"Points: {extraction.Points}");
    Console.WriteLine();
}

// Parse data from Hacker News
var htmlContent = await GetHtmlContent("https://news.ycombinator.com/");
var parseResults = await parseraClient.ParseAsync<ExtractionModel>(htmlContent);

Console.WriteLine("\nParse results:");
counter = 1;
foreach (var extraction in parseResults)
{
    Console.WriteLine($"Extraction {counter++}");
    Console.WriteLine($"Title: {extraction.Title}");
    Console.WriteLine($"Points: {extraction.Points}");
    Console.WriteLine();
}

static async Task<string> GetHtmlContent(string url)
{
    var httpClient = new HttpClient();
    var httpResponse = await httpClient.GetAsync(url);
    return await httpResponse.Content.ReadAsStringAsync();
}

class ExtractionModel
{
    [Extraction("Title", "News title")]
    public string Title { get; set; }

    [Extraction("Points", "Number of points")]
    public int Points { get; set; }
}
