using ControlSystemsLibrary.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlSystemsLibrary.Models.Classes
{
    public class UserInterfacesClass : ViewModelBase
    {
        private string fullUserInterfaceName;
        public string FullUserInterfaceName
        {
            get => fullUserInterfaceName;
            set
            {
                fullUserInterfaceName = value;
                OnPropertyChanged();
            }
        }

        private UserControl userInterfaceControl;
        public UserControl UserInterfaceControl
        {
            get => userInterfaceControl;
            set
            {
                userInterfaceControl = value;
                OnPropertyChanged();
            }
        }
    }
}
