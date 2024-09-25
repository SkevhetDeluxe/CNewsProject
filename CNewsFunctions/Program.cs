using System.Collections.Immutable;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using CNewsFunctions.Data;
using CNewsFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

//services.AddSingleton(new QueueClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "newsletterlist", new QueueClientOptions()
//      {MessageEncoding = QueueMessageEncoding.Base64}));

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<FunctionDbContext>(options => 
            options.UseSqlServer(context.Configuration.GetConnectionString("GlobalConnection")));
        services.AddSingleton(new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage")));
        services.AddSingleton(new QueueServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage")));
        services.AddSingleton(new TableServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage")));
        services.AddScoped<ISuperService, SuperService>();
    })
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("cnewssettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddEnvironmentVariables();
    })
    .Build();

host.Run();
