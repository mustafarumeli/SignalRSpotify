using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Classes.Database.Entities
{
    public class UserRoom
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
    }
}
