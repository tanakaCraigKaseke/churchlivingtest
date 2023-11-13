using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Entities
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credits { get; set; }
        public decimal HoursPerWeek { get; set; }
    }
}
