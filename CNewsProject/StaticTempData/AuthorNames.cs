namespace CNewsProject.StaticTempData;

public static class AuthorNames
{
    public static bool Initialised { get; set; } = false;
    public static List<string> UserNames = new();
}