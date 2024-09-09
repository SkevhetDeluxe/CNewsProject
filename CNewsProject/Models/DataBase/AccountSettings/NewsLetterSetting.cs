using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase.AccountSettings;

public class NewsLetterSetting
{
    [Key]
    public int Id { get; set; }
    public List<int> CategoryIds { get; set; } = new();
    public List<string>? AuthorNames { get; set; }
    public bool Latest { get; set; } = true;
    public bool Popular { get; set; } = true;
}