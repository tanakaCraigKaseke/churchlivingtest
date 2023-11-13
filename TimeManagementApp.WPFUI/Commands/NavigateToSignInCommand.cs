using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Commands
{
    public class NavigateToSignInCommand : BaseCommandAsync
    {
        private readonly INavigationState _navigationState;
        
        private readonly IUserService _userService;

        private readonly IModuleService moduleService;

        public NavigateToSignInCommand(INavigationState navigationState, IUserService userService)
        {
            _navigationState = navigationState;
            _userService = userService;
        } 

        protected override async Task ExecuteAsync(object parameter)
        {
            _navigationState.CurrentViewModel = new SignInVM(_navigationState, _userService, moduleService);
        }
    }
}
