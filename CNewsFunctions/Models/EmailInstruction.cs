namespace CNewsFunctions.Models;

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