using System;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;

namespace CNewsFunctions;

public class BlobResize(ILogger<BlobResize> logger)
{
    [Function("BlobResize")]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        
    }
    /*
     
     
 |\/\/\/|  
 |      |  
 | (o)(o)  
 C      _) 
  |  ___|  
  |   /    
 /____\    
/      \


     */
}