using FluentValidation.Results;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;
using TimeManagementApp.WPFUI.Validation;
using TimeManagementApp.WPFUI.Commands;

namespace TimeManagementApp.WPFUI.ViewModels
{
    public class SignUpVM: BaseViewModel, INotifyDataErrorInfo
    {
        private readonly INavigationState _navigationState;

        private readonly IUserService _userService;

        private readonly SignUpValidation _validator = new SignUpValidation();

        private readonly IModuleService moduleService;
        public ICommand NavigateToSignIn  { get; set; }

        public ICommand SignUp { get; set; }

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

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                _firstName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Password));
            }
        }


        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            { 
                _lastName = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Password));
            }
        }  

        public SignUpVM(INavigationState navigationStatae, IUserService userService, IModuleService moduleService)
        {
            _navigationState = navigationStatae;
            _userService = userService;
            NavigateToSignIn = new NavigateToSignInCommand(_navigationState, _userService);
            this.moduleService = moduleService;
            SignUp = new SignUpCommand(_navigationState, userService, this, moduleService);
          

        }


        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);

            ValidationResult result = _validator.Validate(this, o => o.IncludeProperties(propertyName));

            if (result.Errors.Any())
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
