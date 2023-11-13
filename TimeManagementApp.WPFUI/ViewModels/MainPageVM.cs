using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;

namespace TimeManagementApp.WPFUI.ViewModels
{
    public class MainPageVM : BaseViewModel
    {
          
        private readonly INavigationState _navigationState;
        private readonly IModuleService moduleService;
        private string _loggedInPerson = $"Logged in as: {UserState.LoggedInUser.FirstName} {UserState.LoggedInUser.LastName}";

        private ObservableCollection<UserModuleDto> _userModules;

        public ObservableCollection<UserModuleDto>  UserModules
        {
            get { return _userModules; }
            set { _userModules = value; OnPropertyChanged(); }
        }
          

        public string LoggedInPerson
        {
            get { return _loggedInPerson; }
            set { _loggedInPerson = value; }
        }

        private DateTime filterDate = DateTime.Now;

        public DateTime FilterDate
        {
            get { return filterDate; }
            set { filterDate = value; OnPropertyChanged(); GenerateDateToDisplay(value); }
        }

        private string _filterDisplay;

        public string FilterDisplay
        {
            get { return _filterDisplay; }
            set { _filterDisplay = value; OnPropertyChanged(); }
        }

        public MainPageVM(INavigationState navigationState, IModuleService moduleService)
        {
            _navigationState = navigationState;
            this.moduleService = moduleService;
            GenerateDateToDisplay(DateTime.Now);
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            UserModules = await GetModulesForLoggedInUser();
        } 

        private async Task<ObservableCollection<UserModuleDto>> GetModulesForLoggedInUser()
        {
            var response = await moduleService.GetModulesForLoggedInUser(UserState.LoggedInUser.UserId, FilterDate);

            if (response.IsSuccesfull)
            {
                var data = response.Data as IEnumerable<UserModuleDto>;
                var toDate = new ObservableCollection<UserModuleDto>(data);
                return toDate;
            }else
            {
                MessageBox.Show("Failed to retriev users", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
             
        } 

        private void GenerateDateToDisplay(DateTime value )
        {
            DateTime currentDate = value;
            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);
            string startOfWeekFormatted = startOfWeek.ToString("yyyy-MM-dd");
            string endOfWeekFormatted = endOfWeek.ToString("yyyy-MM-dd");
            FilterDisplay = $"{startOfWeekFormatted}  - {endOfWeekFormatted}";
        }
    }
}
