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

    }
}
