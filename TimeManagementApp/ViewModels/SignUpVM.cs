using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManagementApp.Commands;

namespace TimeManagementApp.ViewModels
{
    internal class SignUpVM : BaseVM
    {

		private string _username;

		public string Username
		{
			get { return _username; }
			set { _username = value; OnPropertyChanged(); }
		}

		private string _password;

		public string Password
		{
			get { return _password; }
			set { _password = value; OnPropertyChanged(); }
		}

		private string _name;

		public string Name
		{ 
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}

		private string _surname;

		public string Surname
		{
			get { return _surname; }
			set { _surname = value; OnPropertyChanged(); }
		}



		public ICommand SignUpCommand { get; set; }


        public SignUpVM()
        {
            SignUpCommand = new SignUpCommand(this);
        }


    }
}
