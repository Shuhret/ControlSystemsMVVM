using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemsLibrary
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //protected bool Set<T>(ref T field, T value, [CallerMemberName] string Property = null)
        //{
        //    if (Equals(field, value))
        //        return false;
        //    OnPropertyChanged(Property);
        //    return true;
        //}
        #endregion

    }
}
