using Microsoft.AspNetCore.SignalR.Client;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.Operations;
using SpeechBubble.Client.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Models
{
    public class ChatRoomViewModel : ViewModelBase
    {
        private ObservableCollection<ChatContent> _messages { get; } = new();
        private HubConnection _connection { get; init; }

        public Guid RoomId { get; init; }
        public IEnumerable<ChatContent> Messages => _messages;

        public ChatRoomViewModel(Guid roomId, HubConnection connection, IEnumerable<Message> messages)
        {
            RoomId = roomId;
            _connection = connection;

            foreach(var msg in messages ?? new List<Message>())
            {
                _messages.Add(msg);
            }

            _connection.On<Common.Data.Message>("ReceiveMessage", async (e) =>
            {
                await Helpers.DispatcherHelper.RunAsync(() => _messages.Add(new Message(e.Content, e.Sender, e.Timestamp)));
            });
        }

        public async Task SendMessageAsync(string message)
            => await _connection.InvokeAsync("SendMessageAllAsync", RoomId, message);
    }
}
