using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Entities
{
    public class Semester : BaseEntity
    {
        public string Name { get; set; }
        public decimal Weeks { get; set; }
        public DateTime StartDate { get; set; }
    }
}
