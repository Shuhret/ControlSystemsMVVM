using ControlSystemsLibrary.ViewModels;
using ControlSystemsLibrary.ViewModels.MainWindowViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlSystemsLibrary.Views
{
    public partial class Administrator : UserControl
    {
        public Administrator(AddSetUserInterface ASUI, Authorization AuthorizationView)
        {
            InitializeComponent();

            AdministratorVM AVM = this.DataContext as AdministratorVM;
            AVM.ASUI = ASUI;
            AVM.AdministratorView = this;
            AVM.AuthorizationView = AuthorizationView;
        }
    }
}
