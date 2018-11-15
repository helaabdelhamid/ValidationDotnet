using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Chat.Web.Models.ViewModels;
using Chat.Web.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using DATA;
using Domaine;
using Domain;

namespace Chat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        #region Properties
        /// <summary>
        /// List of online users
        /// </summary>
        public readonly static List<UserViewModel> _Connections = new List<UserViewModel>();

        /// <summary>
        /// List of available chat rooms
        /// </summary>
        private readonly static List<RoomViewModel> _Rooms = new List<RoomViewModel>();

        /// <summary>
        /// Mapping SignalR connections to application users.
        /// (We don't want to share connectionId)
        /// </summary>
        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        #endregion

        public void Send(string roomName, string message)
        {
            if (message.StartsWith("/private"))
                SendPrivate(message);
            else
                SendToRoom(roomName, message);
        }

        public void SendPrivate(string message)
        {
            // message format: /private(receiverName) Lorem ipsum...
            string[] split = message.Split(')');
            string receiver = split[0].Split('(')[1];
            string userId;
            if (_ConnectionsMap.TryGetValue(receiver, out userId))
            {
                // Who is the sender;
                var sender = _Connections.Where(u => u.Username == IdentityName).First();

                message = Regex.Replace(message, @"\/private\(.*?\)", string.Empty).Trim();

                // Build the message
                MessageViewModel messageViewModel = new MessageViewModel()
                {
                    From = sender.Username,
                    Avatar = "data:image/false;base64,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",
                    To = "",
                    Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                    Timestamp = DateTime.Now.ToLongTimeString()
                };

                // Send the message
                Clients.Client(userId).newMessage(messageViewModel);
                Clients.Caller.newMessage(messageViewModel);
            }
        }

        public void SendToRoom(string roomName, string message)
        {
            try
            {
                using (var db = new EpioneContext())
                {
                    var user = db.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                    var room = db.Rooms.Where(r => r.Name == roomName).FirstOrDefault();

                    // Create and save message in database
                    Message msg = new Message()
                    {
                        Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                        Timestamp = DateTime.Now.Ticks.ToString(),
                        FromUser = user,
                        ToRoom = room
                    };
                    db.Messages.Add(msg);
                    db.SaveChanges();

                    // Broadcast the message
                    var messageViewModel = Mapper.Map<Message, MessageViewModel>(msg);
                    Clients.Group(roomName).newMessage(messageViewModel);
                }
            }
            catch (Exception)
            {
                Clients.Caller.onError("Message not send!");
            }
        }

        public void Join(string roomName)
        {
            try
            {
                var user = _Connections.Where(u => u.Username == IdentityName).FirstOrDefault();
                if (user.CurrentRoom != roomName)
                {
                    // Remove user from others list
                    if (!string.IsNullOrEmpty(user.CurrentRoom))
                        Clients.OthersInGroup(user.CurrentRoom).removeUser(user);

                    // Join to new chat room
                    Leave(user.CurrentRoom);
                    Groups.Add(Context.ConnectionId, roomName);
                    user.CurrentRoom = roomName;

                    // Tell others to update their list of users
                    Clients.OthersInGroup(roomName).addUser(user);
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.onError("You failed to join the chat room!" + ex.Message);
            }
        }

        private void Leave(string roomName)
        {
            Groups.Remove(Context.ConnectionId, roomName);
        }

        public void CreateRoom(string roomName)
        {
            try
            {
                using (var db = new EpioneContext())
                {
                    // Accept: Letters, numbers and one space between words.
                    Match match = Regex.Match(roomName, @"^\w+( \w+)*$");
                    if (!match.Success)
                    {
                        Clients.Caller.onError("Invalid room name!\nRoom name must contain only letters and numbers.");
                    }
                    else if (roomName.Length < 5 || roomName.Length > 20)
                    {
                        Clients.Caller.onError("Room name must be between 5-20 characters!");
                    }
                    else if (db.Rooms.Any(r => r.Name == roomName))
                    {
                        Clients.Caller.onError("Another chat room with this name exists");
                    }
                    else
                    {
                        // Create and save chat room in database
                        var user = db.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                        var room = new Room()
                        {
                            Name = roomName,
                            UserAccount = user
                        };
                        db.Rooms.Add(room);
                        db.SaveChanges();

                        if (room != null)
                        {
                            // Update room list
                            var roomViewModel = Mapper.Map<Room, RoomViewModel>(room);
                            _Rooms.Add(roomViewModel);
                            Clients.All.addChatRoom(roomViewModel);
                        }
                    }
                }//using
            }
            catch (Exception ex)
            {
                Clients.Caller.onError("Couldn't create chat room: " + ex.Message);
            }
        }

        public void DeleteRoom(string roomName)
        {
            try
            {
                using (var db = new EpioneContext())
                {
                    // Delete from database
                    var room = db.Rooms.Where(r => r.Name == roomName && r.UserAccount.UserName == IdentityName).FirstOrDefault();
                    db.Rooms.Remove(room);
                    db.SaveChanges();

                    // Delete from list
                    var roomViewModel = _Rooms.First<RoomViewModel>(r => r.Name == roomName);
                    _Rooms.Remove(roomViewModel);

                    // Move users back to Lobby
                    Clients.Group(roomName).onRoomDeleted(string.Format("Room {0} has been deleted.\nYou are now moved to the Lobby!", roomName));

                    // Tell all users to update their room list
                    Clients.All.removeChatRoom(roomViewModel);
                }
            }
            catch (Exception)
            {
                Clients.Caller.onError("Can't delete this chat room.");
            }
        }

        public IEnumerable<MessageViewModel> GetMessageHistory(string roomName)
        {
            using (var db = new EpioneContext())
            {
                var messageHistory = db.Messages.Where(m => m.ToRoom.Name == roomName)
                    .OrderByDescending(m => m.Timestamp)
                    .Take(20)
                    .AsEnumerable()
                    .Reverse()
                    .ToList();

                return Mapper.Map<IEnumerable<Message>, IEnumerable<MessageViewModel>>(messageHistory);
            }
        }

        public IEnumerable<RoomViewModel> GetRooms()
        {
            using (var db = new EpioneContext())
            {
                // First run?
                if (_Rooms.Count == 0)
                {
                    foreach (var room in db.Rooms)
                    {
                        var roomViewModel = Mapper.Map<Room, RoomViewModel>(room);
                        _Rooms.Add(roomViewModel);
                    }
                }
            }

            return _Rooms.ToList();
        }

        public IEnumerable<UserViewModel> GetUsers(string roomName)
        {
            return _Connections.Where(u => u.CurrentRoom == roomName).ToList();
        }

        #region OnConnected/OnDisconnected
        public override Task OnConnected()
        {
            using (var db = new EpioneContext())
            {
                try
                {
                    var user = db.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();

                    var userViewModel = Mapper.Map<User, UserViewModel>(user);
                    userViewModel.Device = GetDevice();
                    userViewModel.CurrentRoom = "";

                    _Connections.Add(userViewModel);
                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);

                    Clients.Caller.getProfileInfo(user.UserName, "data:image/false;base64,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");
                }
                catch (Exception ex)
                {
                    Clients.Caller.onError("OnConnected:" + ex.Message);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                var user = _Connections.Where(u => u.Username == IdentityName).First();
                _Connections.Remove(user);

                // Tell other users to remove you from their list
                Clients.OthersInGroup(user.CurrentRoom).removeUser(user);

                // Remove mapping
                _ConnectionsMap.Remove(user.Username);
            }
            catch (Exception ex)
            {
                Clients.Caller.onError("OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var user = _Connections.Where(u => u.Username == IdentityName).First();
            Clients.Caller.getProfileInfo(user.DisplayName, user.Avatar);

            return base.OnReconnected();
        }
        #endregion

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

        private string GetDevice()
        {
            string device = Context.Headers.Get("Device");

            if (device != null && (device.Equals("Desktop") || device.Equals("Mobile")))
                return device;

            return "Web";
        }
    }
}