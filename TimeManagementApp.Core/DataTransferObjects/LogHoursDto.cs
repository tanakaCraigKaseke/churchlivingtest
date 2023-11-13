using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.DataTransferObjects
{
    public class LogHoursDto
    {
        public int ModuleId { get; set; }
        public decimal HoursSpent { get; set; }
        public DateTime Date { get; set; } 

    }
}
