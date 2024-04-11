using Microsoft.AspNetCore.SignalR.Client;
using Prism.Events;
using SpeechBubble.Client.Events;
using SpeechBubble.Client.Models;
using SpeechBubble.Client.Operations;
using SpeechBubble.Client.Services.Base;
using SpeechBubble.Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SpeechBubble.Client.Services
{
    public class ChatService : IChatService
    {
        public List<ChatRoomViewModel> ChatRooms { get; } = new List<ChatRoomViewModel>();

        public async Task JoinServerRoomsAsync(ConnectedEventArgs args)
        {
            foreach (Guid roomId in args.RoomsIdCollection)
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl($"{args.ServerUrl}/{roomId}", configureHttpConnection: options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(args.AccessToken)!;
                    })
                    .WithAutomaticReconnect()
                    .Build();

                var roomOperations = new RoomOperations();                

                var response = await roomOperations.GetMessagesAsync(roomId.ToString());
                ChatRooms.Add(new ChatRoomViewModel(roomId, connection, response.Messages.Select(message => new Models.Message(message.Content, message.Sender, DateTime.Now))));
                await connection.StartAsync();
            }
        }
        public ChatRoomViewModel GetRoomById(Guid id)
        => ChatRooms.First(r => r.RoomId == id);
        public async void SendMessage(ChatRoomViewModel room, string message)
            => await room.SendMessageAsync(message);
    }
}
