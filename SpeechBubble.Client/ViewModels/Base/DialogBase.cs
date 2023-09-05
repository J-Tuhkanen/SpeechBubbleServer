using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBubble.Client.ViewModels.Base
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        private bool _isOpen;

        public bool IsOpen
        {
            get => _isOpen;
            private set => Set(ref _isOpen, value);
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
