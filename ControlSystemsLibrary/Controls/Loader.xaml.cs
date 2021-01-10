using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ControlSystemsLibrary.Controls
{

    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();

            Thread thread = new Thread(new ThreadStart(RunAnimation));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }


        void RunAnimation()
        {
            Action ac = () =>
            {
                Storyboard sb = this.FindResource("loop") as Storyboard; // AAA-Название анимации
                sb.Begin(); // Запустить анимацию
            };
            Dispatcher.Invoke(ac);
        }
    }
}
