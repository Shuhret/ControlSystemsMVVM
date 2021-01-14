using ControlSystemsLibrary.ViewModels;
using ControlSystemsLibrary.ViewModels.MainWindowViewModel;
using System.Windows.Controls;

namespace ControlSystemsLibrary.Views
{

    public partial class Authorization : UserControl
    {
        public Authorization(AddSetUserInterface ASUI)
        {
            InitializeComponent();

            AuthorizationVM AVM = this.DataContext as AuthorizationVM;
            AVM.ASUI = ASUI;
            AVM.AuthorizationView = this;
        }
    }
}
