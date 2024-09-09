using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using CNewsProject.Helpers;

namespace CNewsFunctions;

public class NewsLetter
{
    private readonly ILogger<NewsLetter> _logger;

    public NewsLetter(ILogger<NewsLetter> logger)
    {
        _logger = logger;
    }

    [Function("NewsLetter")]
    public void Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer)
    {
        
    }
}