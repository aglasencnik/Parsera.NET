using Microsoft.Extensions.Configuration;
using Parsera;
using Parsera.Models;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var parseraClient = new ParseraClient(configuration["ParseraApiKey"] ?? string.Empty);

// Display first 10 proxy countries
var proxyCountries = await parseraClient.GetProxyCountriesAsync();

Console.WriteLine("Proxy countries:");
foreach (var country in proxyCountries.Take(10))
{
    Console.WriteLine($"{country.FriendlyName} ({country.SystemName})");
}

// Extract data from Hacker News
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

var result = await parseraClient.ExtractAsync(extractionRequest);

Console.WriteLine("\nExtraction result:");
var counter = 1;
foreach (var extraction in result.Data)
{
    Console.WriteLine($"Extraction {counter++}");
    foreach (var attribute in extraction.ToList())
    {
        Console.WriteLine($"{attribute.Key}: {attribute.Value}");
    }
    Console.WriteLine();
}
