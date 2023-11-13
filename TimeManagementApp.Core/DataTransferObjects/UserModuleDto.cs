using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.DataTransferObjects
{
    public class UserModuleDto
    { 
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credits  { get; set; }
        public  decimal HoursPerWeek { get; set; }
        public string SemesterName { get; set; }
        public decimal Weeks { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalHoursSpent { get; set; }
        public decimal HoursRemaining { get; set; }
        public decimal SelfStudyHoursPerWeek { get; set; }
    } 
}
