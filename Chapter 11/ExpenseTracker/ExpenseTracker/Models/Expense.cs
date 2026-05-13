using ExpenseTracker.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [MaxPastDate(3)]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required]
        [Display(Name = "Expense Type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExpenseType? ExpenseType { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Vendor { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "The {0} field must be <= {2}")]
        public decimal? Amount { get; set; }

        public bool Paid { get; set; }

    }
}
