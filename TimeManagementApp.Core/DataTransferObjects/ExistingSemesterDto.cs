using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.DataTransferObjects
{
     public class ExistingSemesterDto
    {
        public int SemesterId { get; set; }
        public string Name { get; set; }
        public decimal Weeks { get; set; }
        public DateTime StartDate { get; set; }
    }
}
