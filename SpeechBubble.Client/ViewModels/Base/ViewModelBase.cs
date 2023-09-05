using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpeechBubble.Client.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(T currentValue, T newValue, Action<T> onSetter, [CallerMemberName] string propertyName = null)
        {
            if (Equals(currentValue, newValue))
                return false;

            onSetter?.Invoke(newValue);

            RaisePropertyChanged(propertyName);

            return true;
        }

        protected virtual bool Set<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(currentValue, newValue))
                return false;

            currentValue = newValue;

            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
