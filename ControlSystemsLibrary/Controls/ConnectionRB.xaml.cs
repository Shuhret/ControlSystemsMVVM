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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlSystemsLibrary.Controls
{
    [TemplatePart(Name = "PART_DeleteButton", Type = typeof(Button))]

    public partial class ConnectionRB : RadioButton
    {
        public event EventHandler Deleted;
        public ConnectionRB()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var deleteButton = this.GetTemplateChild("PART_DeleteButton") as Button;
            if (deleteButton != null)
            {
                deleteButton.Click += DeleteButton_Click;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Deleted != null)
                Deleted(this, e);
        }


    }
}
