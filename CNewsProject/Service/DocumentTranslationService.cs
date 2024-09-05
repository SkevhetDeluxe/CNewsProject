using Azure;
using Azure.AI.Translation.Document;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public class DocumentTranslationService
    {
        private readonly string _endpoint = "https://<your-resource-name>.cognitiveservices.azure.com/";
        private readonly string _subscriptionKey = "<your-subscription-key>";// || "<my-subscription-key>";

        //public async Task TranslateDocumentAsync(string inputContainerUri, string outputContainerUri, string targetLanguage)
        //{
        //    Uri endpoint = new Uri(_endpoint);
        //    AzureKeyCredential credential = new AzureKeyCredential(_subscriptionKey);
        //    DocumentTranslationClient client = new DocumentTranslationClient(endpoint, credential);

        //    var translationInputs = new List<DocumentTranslationInput>
        //{
        //    //new DocumentTranslationInput(
        //    //    //new TranslationSource(new Uri(inputContainerUri)),
        //    //    new TranslationTarget(new Uri(outputContainerUri))
        //    //    {
        //    //        LanguageCode = targetLanguage
        //    //    })
        //};

        //    DocumentTranslationOperation operation = await client.StartTranslationAsync(translationInputs);

        //    // Wait for the operation to complete
        //    await operation.WaitForCompletionAsync();

        //    // Check the status
        //    if (operation.HasCompleted)
        //    {
        //        Console.WriteLine("Document translation completed successfully.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Document translation failed.");
        //    }
        //}
    }
}
