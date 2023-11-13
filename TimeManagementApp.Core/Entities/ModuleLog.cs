using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Entities
{
    public class ModuleLog : BaseEntity
    {
        public decimal HoursSpent { get; set; }
        public DateTime Date { get; set; }
    }
}
