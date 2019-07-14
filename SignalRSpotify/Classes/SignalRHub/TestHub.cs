using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRSpotify.Classes.Database;
using SignalRSpotify.Classes.Database.Entities;
using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SignalRSpotify.Classes.SignalRHub
{
    public class TestHub : Hub
    {
        SandSContext _db;
        public TestHub(SandSContext db)
        {
            _db = db;
        }
        User GetCurrentUser(string connectionId)
        {
            string userId = _db.UserConnectionIds.FirstOrDefault(y => y.ConnectionId == connectionId).Id;
            return _db.Users.Find(userId);
        }
        public string CreateOrJoinRoom(string roomName, bool isCreate)
        {
            string connectionId = Context.ConnectionId;
            User user = GetCurrentUser(connectionId);
            if (user == null)
            {
                return "user is null error";
            }

            Room room = _db.Rooms.Include(s => s.UserRooms).FirstOrDefault(x => x.RoomName.ToLower().Trim() == roomName.ToLower().Trim());
            if (room != null)
            {
                if (isCreate == true)
                {
                    return "roomName has Taken";
                }
                else
                {
                    if (room.UserRooms.Any(x => x.UserId == user.Id && x.RoomId == room.Id) == false)
                    {
                        room.AddUser(user);
                        _db.SaveChanges();
                    }
                    Groups.AddToGroupAsync(connectionId, roomName);
                    return "Joined " + roomName;
                }
            }
            room = new Room { RoomName = roomName };
            room.AddUser(user);
            _db.Rooms.Add(room);
            _db.SaveChanges();
            Groups.AddToGroupAsync(connectionId, roomName);
            return "Room Created Successfully";
        }

        public async Task SendRoomMessage(string roomName, string message)
        {
            string connectionId = Context.ConnectionId;
            User user = GetCurrentUser(connectionId);
            await Clients.Group(roomName).SendAsync("ReciveGroupMessage", user.UserName, message);
        }
        public async Task WriteText(string text,string roomName)
        {
            string connectionId = Context.ConnectionId;
            await Clients.GroupExcept(roomName,connectionId).SendAsync("AppendText", text);
        }

        public async Task ToggleAudioPlayingStatus(bool isPlaying,string roomName,double currentTime)
        {
            string connectionId = Context.ConnectionId;
            await Clients.GroupExcept(roomName, connectionId).SendAsync("ToggleAudioPlayingStatusJs", isPlaying, currentTime);
        }
        public override Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            string userIdB64 = httpContext.Request.Query["userId"].ToString();
            string userId = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(userIdB64));
            string connectionId = Context.ConnectionId;
            User user = _db.Users.Find(userId);
            UserConnectionId uci = _db.UserConnectionIds.Find(userId);
            if (uci == null)
            {
                uci = new UserConnectionId { User = user, ConnectionId = connectionId };
                _db.UserConnectionIds.Add(uci);
            }
            else
            {
                uci.ConnectionId = connectionId;
                var k = _db.Update(dbObjectEntity: uci);
            }
            _db.SaveChanges();
            return base.OnConnectedAsync();
        }
    }
}
