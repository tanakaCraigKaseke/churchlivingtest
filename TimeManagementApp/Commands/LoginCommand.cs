using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.ViewModels;

namespace TimeManagementApp.Commands
{
    internal class LoginCommand : BaseCommandAsync
    {

       

        protected override async Task ExecuteAsync(object parameter)
        {
            await Task.Run(() => {
                Console.WriteLine("Executing");
            });
        }
    }
}
