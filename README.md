# Parsera.NET

[![NuGet version (Parsera.NET)](https://img.shields.io/nuget/v/Parsera.NET)](https://www.nuget.org/packages/Parsera.NET/)

![GitHub License](https://img.shields.io/github/license/aglasencnik/Parsera.NET)

**Parsera.NET** is a lightweight NuGet package for the Parsera API, designed to simplify interactions and streamline data scraping tasks. This wrapper offers an easy-to-use interface, enabling developers to harness the power of Parsera's capabilities effortlessly. Perfect for projects that require efficient web scraping with minimal overhead, this library ensures quick integration and optimal performance.

> [!IMPORTANT] 
> Parsera API key is required to use the package.

## Installation

To use Parsera.NET in your C# project, you need to install the NuGet package. Follow these simple steps:

### Using NuGet Package Manager

1. **Open Your Project**: Open your project in Visual Studio or your preferred IDE.
2. **Open the Package Manager Console**: Navigate to `Tools` -> `NuGet Package Manager` -> `Package Manager Console`.
3. **Install Parsera.NET**: Type the following command and press Enter:
   `Install-Package Parsera.NET`

### Using .NET CLI

Alternatively, you can use .NET Core CLI to install Parsera.NET. Open your command prompt or terminal and run:

`dotnet add package Parsera.NET`

### Verifying the Installation

After installation, make sure that Parsera.NET is listed in your project dependencies to confirm successful installation.

## Usage

To get started with ParseraClient, create an instance of the `ParseraClient` class and use its methods to interact with the Parsera API.
Below is an example of how you can retireve the list of available proxy countries:

```csharp
var parseraClient = new ParseraClient("api-key");

var proxyCountries = await parseraClient.GetProxyCountriesAsync();

Console.WriteLine("Proxy countries:");
foreach (var country in proxyCountries.Take(10))
{
    Console.WriteLine($"{country.FriendlyName} ({country.SystemName})");
}
```

You can also extract data from a specific website, such as Hacker News:

```csharp
var parseraClient = new ParseraClient("api-key");

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
```

### Dependency Injection and Instancing

There are different ways to create an instance of `ParseraClient` in your project, either by instantiating it directly or using dependency injection (DI) in ASP.NET Core or other DI frameworks.

#### Instantiating Directly

You can create an instance of `ParseraClient` with an API key or options:

```csharp
// With API key
var parseraClient = new ParseraClient("your-api-key");

// With custom options
var options = new ParseraClientOptions
{
    ApiKey = "your-api-key"
};
var parseraClient = new ParseraClient(options);
```

#### Using Dependency Injection (DI)

You can configure `ParseraClient` to be injected via the service container in DI-heavy environments like ASP.NET Core:

1. **With API Key**

```csharp
var builder = Host.CreateApplicationBuilder();

// Add ParseraClient to the service collection with API key
builder.Services.AddParseraClient("your-api-key");

var serviceProvider = builder.Build().Services;
var parseraClient = serviceProvider.GetRequiredService<IParseraClient>();
```

2. **With Custom Options**

```csharp
var builder = Host.CreateApplicationBuilder();

// Add ParseraClient to the service collection with custom options
builder.Services.AddParseraClient(new ParseraClientOptions
{
    ApiKey = _apiKey
});

var serviceProvider = builder.Build().Services;
var parseraClient = serviceProvider.GetRequiredService<IParseraClient>();
```

In both cases, you can retrieve an `IParseraClient` instance from the service provider and use it in your project.

## Support the Project

If you find this project useful, consider supporting it by [buying me a coffee](https://www.buymeacoffee.com/aglasencnik). Your support is greatly appreciated!

## Contributing

Contributions are welcome! If you have a feature to propose or a bug to fix, create a new pull request.

## License

This project is licensed under the [MIT License](https://github.com/aglasencnik/Parsera.NET/blob/main/LICENSE).

## Acknowledgment

This project is inspired by and built upon the [Parsera](https://parsera.org/) project.
