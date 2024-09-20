using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CNewsFunctions;

public class ClearPoppin
{
    private readonly ILogger<ClearPoppin> _logger;

    public ClearPoppin(ILogger<ClearPoppin> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ClearPoppin))]
    public void Run([QueueTrigger("myqueue", Connection = "")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        // TODO THE WHOLE COMPLETE MOTHERFUCKING FUNCTION MAN
    }
}