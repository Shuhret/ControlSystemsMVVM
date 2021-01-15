using ControlSystemsLibrary.ViewModels.Base;
using ControlSystemsLibrary.ViewModels.MainWindowViewModel;
using ControlSystemsLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlSystemsLibrary.ViewModels
{
    
    class AdministratorVM : ViewModelBase
    {
        public AddSetUserInterface ASUI;
        public Authorization AuthorizationView;
        public Administrator AdministratorView;

        // Конструктор
        public AdministratorVM()
        {
            
        }

        public ICommand ClickLockCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ASUI(AuthorizationView);
                });
            }
        }


        // Имя текущего пользователя ----------------------------------------------------------------------------------------
        private string currentUserName = "";
        public string CurrentUserName
        {
            get => currentUserName;
            set
            {
                if (Equals(currentUserName, value)) return;
                currentUserName = value;
                OnPropertyChanged();
            }
        }

        // Название текущего подключения ------------------------------------------------------------------------------------
        private string currentConnectionName;
        public string CurrentConnectionName
        {
            get => currentConnectionName;
            set
            {
                currentConnectionName = value;
                OnPropertyChanged();
            }
        }

        // Текущая строка подключения (зашифровано) -------------------------------------------------------------------------
        private string currentCryptConnectionString = "";
        public string CurrentCryptConnectionString
        {
            get => currentCryptConnectionString;
            set
            {
                if (Equals(currentCryptConnectionString, value)) return;
                currentCryptConnectionString = value;
                OnPropertyChanged();
            }
        }

    }
}
