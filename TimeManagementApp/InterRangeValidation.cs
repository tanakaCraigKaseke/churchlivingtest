using System.Globalization;
using System.Windows.Controls;
using TimeManagementApp;

namespace TimeManagementApp
{
    public class IntegerRangeValidation : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value as string, out int intValue))
            {
                if (intValue >= Min && intValue <= Max)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, $"Please enter an integer between {Min} and {Max}.");
                }
            }
            else
            {
                return new ValidationResult(false, "Please enter a valid integer.");
            }
        }
    }
}
