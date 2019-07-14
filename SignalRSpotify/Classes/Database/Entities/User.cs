using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Classes.Database.Entities
{
    public class User : DbObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [InverseProperty("User")]
        public virtual UserConnectionId UserConnectionId { get; set; } = new UserConnectionId();

        [NotMapped]
        public virtual IEnumerable<Room> Rooms { get { return UserRooms.Where(x => x.User == this).Select(y => y.Room); } }
        public virtual ICollection<UserRoom> UserRooms { get; set; } = new HashSet<UserRoom>();
    }
}
