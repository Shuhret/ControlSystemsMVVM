using ControlSystemsLibrary.ViewModels.Base;
using ControlSystemsLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlSystemsLibrary.ViewModels.MainWindowViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            CurrentUserControl = new Authorization();
        }

        private UserControl currentUserControl;
        public UserControl CurrentUserControl
        {
            get => currentUserControl;
            set
            {
                currentUserControl = value;
                OnPropertyChanged();
            }

        }
    }
}
