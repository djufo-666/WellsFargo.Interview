// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WellsFargo.Interview.Bootstraps;
using WellsFargo.Interview.ConsoleApp;


Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB", false);

Console.WriteLine("Starting Wells Fargo Interview Task app !!!");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var serviceCollection = new ServiceCollection();

_ = serviceCollection
    .AddTransient<IConfiguration>(serviceProvider => configuration)
    .AddLogging(builder =>
    {
        _ = builder
            .AddDebug()
            .AddConsole()
            ;
    })
    .AddServices()
    .AddSingleton<App>();
;

var serviceProvider = serviceCollection
    .BuildServiceProvider();

App app = serviceProvider.GetService<App>();

app.RunAsync()
    .GetAwaiter()
    .GetResult();

Console.WriteLine("Finished !!!");
