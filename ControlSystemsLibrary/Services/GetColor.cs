using System.Windows;
using System.Windows.Media;

namespace ControlSystemsLibrary.Services
{
    class GetColor
    {
        public static SolidColorBrush Get(string ColorName)
        {
            return new SolidColorBrush((Color)Application.Current.FindResource(ColorName));
        }
    }
}
