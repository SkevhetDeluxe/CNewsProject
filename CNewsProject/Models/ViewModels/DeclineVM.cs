namespace CNewsProject.Models.ViewModels
{
    public class DeclineVM
    {
        public int Id { get; set; }
        public string Reason { get; set; } = "Generic Reason for Decline";
        public string HeadLine { get; set; } = "Unassigned";
    }
}
