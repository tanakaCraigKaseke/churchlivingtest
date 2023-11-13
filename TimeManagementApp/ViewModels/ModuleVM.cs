using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.ViewModels
{
    internal class ModuleVM :BaseVM
    {
        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private int _credits;

        public int Credits
        {
            get { return _credits; }
            set { _credits = value; OnPropertyChanged(); }
        }

        private int _classhours;

        public int ClassHoursPerWeek
        {
            get { return _classhours; }
            set { _classhours = value; OnPropertyChanged(); }
        }
    }
}
