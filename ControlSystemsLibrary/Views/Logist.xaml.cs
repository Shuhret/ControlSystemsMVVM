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
    /// <summary>
    /// Логика взаимодействия для Logist.xaml
    /// </summary>
    public partial class Logist : UserControl
    {
        public Logist(AddSetUserInterface ASUI, Authorization AuthorizationView)
        {
            InitializeComponent();

            LogistVM LVM = this.DataContext as LogistVM;
            LVM.ASUI = ASUI;
            LVM.LogistView = this;
            LVM.AuthorizationView = AuthorizationView;
        }
    }
}
