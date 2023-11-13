using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Helpers;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Commands
{
    public class SignUpCommand : BaseCommandAsync
    {
        private readonly INavigationState _navigationState;
        private readonly IUserService _userService;
        private IModuleService moduleService;
        private readonly SignUpVM _viewModel;
        public SignUpCommand(INavigationState  navigationState, IUserService userService, SignUpVM viewModel, IModuleService moduleService)
        {
            _navigationState = navigationState;
            _userService = userService;
            _viewModel = viewModel;
            this.moduleService = moduleService;

        }

        protected override async Task ExecuteAsync(object parameter)
        {

            _viewModel.Validate();
            if (_viewModel.HasErrors)
            {
                return;  
            }

            var register = new RegisterDto
            {
                Username = _viewModel.Username,
                FirstName = _viewModel.FirstName,
                LastName = _viewModel.LastName,
                PasswordHash = PasswordManager.GeneratePasswordHash(_viewModel.Password)
            };

            var response = await _userService.RegisterUser(register);

            if(response.IsSuccesfull)
            {
              var dialogResult =   MessageBox.Show(response.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if(dialogResult == MessageBoxResult.OK)
                {
                    _navigationState.CurrentViewModel = new SignInVM(_navigationState, _userService, moduleService);
                }
            
            }
            else
            {
                MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await Task.Run(() => { });
        } 
    }
}
