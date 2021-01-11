using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ControlSystemsLibrary.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
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


        private bool _Disposed;
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            // Освобождение управляемых ресурсов
        }
        #endregion

    }
}
