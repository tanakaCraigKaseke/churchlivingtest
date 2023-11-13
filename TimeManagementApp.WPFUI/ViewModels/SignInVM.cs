using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Commands;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;
using TimeManagementApp.WPFUI.Validation;
 

namespace TimeManagementApp.WPFUI.ViewModels
{
    public class SignInVM : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly INavigationState _navigationState;

        private readonly IUserService _userService;

        private readonly IModuleService _moduleService;

  

        private readonly LoginValidation _validator = new LoginValidation();

        public ICommand NavigateToSignUp { get; set; }
        public ICommand Login { get; set; }

        private string _username;

        public string Username
        {
            get { return _username; }
            set 
            {
                _username = value; 
                OnPropertyChanged(); 
                ValidateProperty(nameof(Username));
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value; 
                OnPropertyChanged(); 
                ValidateProperty(nameof(Password)); 
            }
        }

        public SignInVM(INavigationState navigationState, IUserService userService, IModuleService moduleService)
        {
            _navigationState = navigationState;
            _userService = userService;
            NavigateToSignUp = new NavigateToSignUpCommand(_navigationState, _userService);
            Login = new LoginCommand(this, _navigationState, _userService, moduleService);        
        } 

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);

            ValidationResult result = _validator.Validate(this, o => o.IncludeProperties(propertyName));

            if(result.Errors.Any())
            {
                string firstErrorMessage = result.Errors.First().ErrorMessage;

                AddError(propertyName, firstErrorMessage);
            }
        }   

        public void Validate()
        {
            ClearErrors();

            ValidationResult result = _validator.Validate(this);

            IEnumerable<ValidationFailure> errors = result.Errors;

            foreach (ValidationFailure error in errors)
            {
                AddError(error.PropertyName, error.ErrorMessage);
            }
        }




        #region INotifyDataErroInfo
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            _propertyErrors.TryGetValue(propertyName, out var errors);
            return errors;
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors()
        {
            _propertyErrors.Clear();
            OnErrorsChanged();
        }

        public void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

    }
}
  