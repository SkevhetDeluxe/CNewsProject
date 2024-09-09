using System.Collections.Immutable;
using Azure.Storage.Blobs;
using CNewsProject.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<FunctionDbContext>(options => 
            options.UseSqlServer(context.Configuration.GetConnectionString("GlobalConnection")));
        services.AddSingleton(new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage")));
    })
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
    })
    .Build();

host.Run();
