using System.Collections.ObjectModel;
using ControlSystemsLibrary.Models.Classes;
using ControlSystemsLibrary.ViewModels.Base;
using ControlSystemsLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlSystemsLibrary.ViewModels.MainWindowViewModel
{
    public delegate void AddSetUserInterface(UserControl UserInterface);
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            AddSetUserInterface ASUI = SetUserINterface;
            CurrentUserInterface = new Authorization(ASUI);
        }

        void SetUserINterface(UserControl UserInterface)
        {
            CurrentUserInterface = UserInterface;
        }


        private UserControl currentUserInterface;
        public UserControl CurrentUserInterface
        {
            get => currentUserInterface;
            set
            {
                currentUserInterface = value;
                OnPropertyChanged();
            }
        }


    }
}
