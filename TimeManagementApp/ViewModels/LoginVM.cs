using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManagementApp.Commands;

namespace TimeManagementApp.ViewModels
{
    internal class LoginVM : BaseVM
    {
		private string _email;

		public string Email
		{
			get { return _email; }
			set 
			{
				_email = value;
				OnPropertyChanged();
			}
		}


		private string _password;

		public string Password
		{
			get { return _password; }
			set {
				_password = value;
				OnPropertyChanged();
			}
		}


        public ICommand LoginCommand { get; set; }


        public LoginVM()
        {
			LoginCommand = new LoginCommand();
        }

      
        }

    }

