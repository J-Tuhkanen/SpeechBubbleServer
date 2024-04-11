using Prism.Commands;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpeechBubble.Client.ViewModels
{
    public class RoomListViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly OpenRoomEvent _openRoomEvent;
        private ObservableCollection<Guid> _roomIdCollection = new();

        public ICommand OpenRoomCommand { get; }
        public IEnumerable<Guid> RoomIdCollection => _roomIdCollection;

        public RoomListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ConnectedEvent>().Subscribe(OnConnectedEvent);
            _openRoomEvent = _eventAggregator.GetEvent<OpenRoomEvent>();
            OpenRoomCommand = new DelegateCommand<Guid?>(OnOpenRoomCommandExecute);
        }

        private void OnOpenRoomCommandExecute(Guid? roomId)
        {
            if (roomId.HasValue)
            {
                _openRoomEvent.Publish(new OpenRoomEventArgs { RoomId = roomId.Value });
            }
        }
        
        private void OnConnectedEvent(ConnectedEventArgs args)
        {
            Helpers.DispatcherHelper.RunAsync(async () =>
            {
                _roomIdCollection = new ObservableCollection<Guid>(args.RoomsIdCollection);
            });
        }
    }
}
