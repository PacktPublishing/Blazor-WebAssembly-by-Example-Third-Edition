namespace SmartComponents.Models
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }
}