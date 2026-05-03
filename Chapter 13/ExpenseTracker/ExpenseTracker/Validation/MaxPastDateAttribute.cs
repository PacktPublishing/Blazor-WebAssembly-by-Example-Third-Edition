using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Validation
{
    public class MaxPastDateAttribute : ValidationAttribute
    {
        private readonly int _months;

        public MaxPastDateAttribute(int months)
        {
            _months = months;
            ErrorMessage = $"{{0}} cannot be more than {_months} months in the past.";
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value is DateOnly date)
            {
                var minDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(-_months));

                if (date < minDate)
                {
                    return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName),
                    validationContext.MemberName is null
                        ? null
                        : [validationContext.MemberName]);
                }
            }

            return ValidationResult.Success;
        }
    }

}
