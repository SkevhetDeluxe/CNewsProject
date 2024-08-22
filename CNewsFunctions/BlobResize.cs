using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace CNewsFunctions;

public class BlobResize
{
    private readonly ILogger<BlobResize> _logger;

    public BlobResize(ILogger<BlobResize> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobResize))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        
    }
}