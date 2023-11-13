using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.DataTransferObjects
{
    public class DataResponseDto
    {
        public bool IsSuccesfull { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
