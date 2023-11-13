using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Commands
{
    public class LoginCommand : BaseCommandAsync
    {
        private SignInVM _signInVM;
        private INavigationState _navigationState;
        private readonly IModuleService _moduleService;

 

        private readonly IUserService _userService;

        public LoginCommand(SignInVM signInVM, INavigationState navigationState, IUserService userService, IModuleService moduleService)
        {
            _signInVM = signInVM;
            _navigationState = navigationState;
            _userService = userService;
            _moduleService = moduleService;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _signInVM.Validate();
            if(_signInVM.HasErrors)
            {
                return;
            }

            var loginDto = new LoginDto
            {
                Username = _signInVM.Username,
                Password = _signInVM.Password,
            }; 

            var response = await _userService.LoginUser(loginDto);

            if (response.IsSuccesfull)
            {
                var confirmResult = MessageBox.Show(response.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                if (confirmResult == MessageBoxResult.OK)
                {
                    UserState.IsLoggedIn = true;
                    UserState.LoggedInUser = response.Data as LoggedInUserDto;

                    _navigationState.CurrentViewModel = new MainPageVM(_navigationState, _moduleService);
                }
            }
            else
            {
                MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

        }
    }
}
