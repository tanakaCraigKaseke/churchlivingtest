using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;

namespace TimeManagementApp.WPFUI.State
{
    public static class UserState
    {
        public static bool IsLoggedIn { get; set; }

        public static LoggedInUserDto  LoggedInUser { get; set; }

    } 
}
 