using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI.Interfaces
{
    public interface INavigationState
    {
        event Action CurrentViewModelChanged;

        BaseViewModel CurrentViewModel { get; set; }
    }
}
