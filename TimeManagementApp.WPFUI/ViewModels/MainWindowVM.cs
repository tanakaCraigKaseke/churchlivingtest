using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;

namespace TimeManagementApp.WPFUI.ViewModels
{
    public class MainWindowVM : BaseViewModel
    {
        private readonly INavigationState _navigationState;

        public BaseViewModel CurrentViewModel => _navigationState.CurrentViewModel;

        public MainWindowVM(INavigationState navigationState)
        {
            _navigationState = navigationState;
            _navigationState.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        } 
    }
}
  