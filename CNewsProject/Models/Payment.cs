namespace CNewsProject.Models
{
    public class Payment
    {
        public int Amount { get; set; } 
        public string Currency {  get; set; }   
        public string Description { get; set; } 
    }
    public class ChangeViewModel
    {
        public required string ChargeId { get; set; }   
    }
}
