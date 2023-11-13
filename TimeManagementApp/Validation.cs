using System;
using System.Linq;

namespace TimeManagementApp
{
    public class Validation
    {
        
        public static bool ValidateStudyDate(DateTime selectedDate, DateTime semesterStartDate, int numberOfWeeks)
        {
            // Calculate the semester end date based on the start date and number of weeks
            DateTime semesterEndDate = semesterStartDate.AddDays(numberOfWeeks * 7);

            return selectedDate >= semesterStartDate && selectedDate <= semesterEndDate;
        }

        public static bool IsValidModuleCode(string code)
        {
            // Check if the code is exactly 8 characters long
            if (code.Length != 8)
            {
                return false;
            }

            // Check if the code consists of 4 letters followed by 4 digits
            return code.Take(4).All(char.IsLetter) && code.Skip(4).All(char.IsDigit);
        }

        public static bool ValidateHoursSpent(string hoursSpentText, out double hoursSpent)
        {
            if (double.TryParse(hoursSpentText, out hoursSpent))
            {
                return hoursSpent >= 0.0;
            }
            return false;
        }
    }
}
