using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Commands
{
    public class NavigateToSignUpCommand: BaseCommandAsync
    {
        private readonly INavigationState _navigationState;
        private readonly IUserService userService;

        private readonly IModuleService moduleService;

        public NavigateToSignUpCommand(INavigationState navigationState, IUserService userService)
        {
            _navigationState = navigationState;
            this.userService = userService;
        }

 
        protected override async Task ExecuteAsync(object parameter)
        {
            Console.WriteLine(_navigationState);
            _navigationState.CurrentViewModel = new SignUpVM(_navigationState, userService, moduleService);
        }
    }
}  
  