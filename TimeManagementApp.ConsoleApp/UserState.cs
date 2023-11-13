using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;

namespace TimeManagementApp.ConsoleApp
{
    public class UserState
    {
        public bool IsLoggedIn { get; set; }
        public LoggedInUserDto  loggedInUser { get; set; }
    }
}
