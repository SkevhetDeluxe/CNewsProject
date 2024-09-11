using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CNewsFunctions;

public class BlobResize(ILogger<BlobResize> logger, BlobServiceClient serviceClient, IConfiguration config)
{
    //private readonly string[] _targetContainers = new[] { "images", "medium-images", "small-images" };

    [Function("BlobResize")]
    public async Task RunAsync([BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")] BlobClient blobClient,
        string name)
    {
        try
        {
            logger.LogInformation("Realised blob in container images");

            // Download the original image from the blob
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (var imageStream = new MemoryStream())
            {
                imageStream.Position = 0;
                // Copy the blob content to a memory stream
                await download.Content.CopyToAsync(imageStream);


                var containerSection = config.GetSection("containerNames");

                foreach (var child in containerSection.GetChildren())
                {
                    imageStream.Position = 0;
                    await ResizeUploadToContainerAsync(child.Key, child.Value, name, imageStream);
                }
            }

            logger.LogInformation($"Image {name} resized and uploaded to multiple containers successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error resizing and uploading image {name}: {ex.Message}");
        }
    }

    private string GetBlobUri(BlobClient blobClient)
    {
        return blobClient.Uri.AbsoluteUri;
    }

    private async Task ResizeUploadToContainerAsync(string imgSize, string containerName, string blobName,
        Stream imageStream)
    {
        try
        {
            using (Image image = Image.Load(imageStream))
            {
                //Get X and Y pixels from local.settings.json CONFIG


                var x = config.GetValue<int>($"imgSizes:{imgSize}:x");
                var y = config.GetValue<int>($"imgSizes:{imgSize}:y");

                image.Mutate(i => i.Resize(x, y));


                // Create a memory stream to hold the resized image
                using (var outputStream = new MemoryStream())
                {
                    outputStream.Position = 0;
                    // Save the resized image to the output stream (JPEG format)
                    await image.SaveAsJpegAsync(outputStream);


                    BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);

                    // Ensure the container exists
                    await containerClient.CreateIfNotExistsAsync();

                    // Get a reference to the blob in the target container
                    BlobClient resizedBlobClient = containerClient.GetBlobClient(blobName);




                    // Upload the resized image to the blob container
                    imageStream.Position = 0;
                    outputStream.Position = 0;
                    await resizedBlobClient.UploadAsync(outputStream, overwrite: true);

                    logger.LogInformation($"Uploaded image {blobName} to container {containerName}.");

                    string imageUrl = GetBlobUri(resizedBlobClient);
                    logger.LogInformation($"Image URL: {imageUrl}");

                    outputStream.Position = 0;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error uploading image {blobName} to container {containerName}: {ex.Message}");
        }
    }
    /*


 |\/\/\/| 
 | FUCK |
 | (o)(o)
 C      _)
  |  ___|
  |   /
 /____\
/      \


     */
}