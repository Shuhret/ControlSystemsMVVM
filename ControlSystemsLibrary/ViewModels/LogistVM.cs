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
    class LogistVM : ViewModelBase
    {
        public AddSetUserInterface ASUI;
        public Authorization AuthorizationView;
        public Logist LogistView;

        public LogistVM()
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
