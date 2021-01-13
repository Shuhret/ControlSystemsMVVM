using ControlSystemsLibrary.Models.Classes;
using ControlSystemsLibrary.ViewModels;
using ControlSystemsLibrary.ViewModels.MainWindowViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
