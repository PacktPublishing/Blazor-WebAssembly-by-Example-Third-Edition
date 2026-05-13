using System.ComponentModel.DataAnnotations;

namespace Ch11_App.Models
{
    public class Expense
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Vendor { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "The Amount must be <= 500 to be approved")]
        public decimal? Amount { get; set; }

    }
}
