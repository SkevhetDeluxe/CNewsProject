using Azure.Storage.Queues;
using CNewsProject.Models.Schedule;
using Newtonsoft.Json;

namespace CNewsProject.TimedServices;

public class TimedNewsLetterService(ILogger<TimedNewsLetterService> logger, QueueServiceClient queueServiceClient, IServiceScopeFactory scopeFactory) : BackgroundService
{
    private bool _sentToday = false;
    private string _lastSent = string.Empty;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Timed News Letter Service running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Timed News Letter Service : >>> SENDING QUEUE MESSAGE <<<");
            var queueClient = queueServiceClient.GetQueueClient("devqueuetwo");
            await queueClient.SendMessageAsync($"Logging {DateTime.Now}");

            if (isOnSchedule())
                Work();
            
            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }

        logger.LogInformation("Timed News Letter Service is stopping.");
    }

    private bool isOnSchedule()
    {
        ScheduleConfig schedule = JsonConvert.DeserializeObject<ScheduleConfig>(File.ReadAllText("timerschedule.json"))!;
        
        var weekday = DateTime.Now.DayOfWeek.ToString();
        int hour = DateTime.Now.Hour;
        int minutes = DateTime.Now.Minute;

        var queueClient = queueServiceClient.GetQueueClient("devqueueone");

        if (_lastSent != weekday)
        {
            _lastSent = weekday;
            if (weekday == schedule.Weekday)
            {
                if (hour > schedule.Hour)
                {
                    if (minutes >= schedule.Minute && _sentToday == false)
                    {
                        queueClient.SendMessage("Activated SCHEDULE");
                        _sentToday = true;
                        return true;
                    }
                }
            } 
        }
        
            
        return false;
    }

    private void Work()
    {
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var service = scope.ServiceProvider.GetRequiredService<INewsLetterService>();


            // Fetching USERS
            var users = service.GetEmailUserList();

            // Fetching RECENT ArticleList
            var recentArticles = service.GetRecentArticleList();

            // Constructing The Instructions
            List<EmailInstruction> emailInstructions = new();
            int count = 0;
            int totalCount = users.Count();
            if (totalCount == 0)
            {
                foreach (var user in users)
                {
                    emailInstructions.Add(new EmailInstruction()
                    {
                        AmountOfMessages = totalCount,
                        NumberInList = count++,
                        Email = user.Email,
                        UserName = user.UserName,
                        Subject = "Weekly News Letter",
                        ArticleIds = service.GetUserNewsLetterArticles(user, recentArticles),
                        AuthorNames = user.AuthorNames ?? new(),
                    });
                }
            }
            else
            {
                emailInstructions.Add(new EmailInstruction()
                {
                    AmountOfMessages = 1,
                    NumberInList = 0,
                    Email = "NOMAIL",
                    UserName = "IGNORE",
                    Subject = "IGNORE",
                    ArticleIds = new(),
                    AuthorNames = new()
                });
            }

            var queueClient = queueServiceClient.GetQueueClient("newsletterlist");
            // SENDING the INSTRUCTIONS!!!
            foreach (var instruction in emailInstructions)
            {
                queueClient.SendMessage((JsonConvert.SerializeObject(instruction)));
            }
        }
    }
}

public class EmailInstruction
{
    // TO WHO!?
    public int NumberInList { get; set; }
    public int AmountOfMessages { get; set; }
    public string Email { get; set; } = "INIT";
    public string UserName { get; set; } = "INIT";
    public string Subject { get; set; } = "INIT";
    public List<int> ArticleIds { get; set; } = new();
    public List<string> AuthorNames { get; set; } = new();
}