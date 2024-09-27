namespace CNewsProject.Models.Schedule;

public class ScheduleConfig
{
    public string Weekday { get; set; } = string.Empty;
    public int Hour { get; set; }
    public int Minute { get; set; }
}