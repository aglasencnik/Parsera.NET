using Microsoft.Extensions.Configuration;
using Parsera;

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
var result = await parseraClient.ExtractAsync<ExtractionModel>("https://news.ycombinator.com/", "UnitedStates");

Console.WriteLine("\nExtraction result:");
var counter = 1;
foreach (var extraction in result)
{
    Console.WriteLine($"Extraction {counter++}");
    Console.WriteLine($"Title: {extraction.Title}");
    Console.WriteLine($"Points: {extraction.Points}");
    Console.WriteLine();
}

class ExtractionModel
{
    [Extraction("Title", "News title")]
    public string Title { get; set; }

    [Extraction("Points", "Number of points")]
    public int Points { get; set; }
}
