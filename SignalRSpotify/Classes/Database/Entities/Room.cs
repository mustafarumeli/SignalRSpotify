using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Classes.Database.Entities
{
    public class Room : DbObject
    {
        public string RoomName { get; set; }


        [NotMapped]
        public virtual IEnumerable<User> Users => UserRooms.Where(x => x.RoomId == Id).Select(y => y.User);

        public void AddUser(User user)
        {

            var ur = new UserRoom
            {
                RoomId = Id,
                UserId = user.Id
            };
            UserRooms.Add(ur);


        }
        public virtual ICollection<UserRoom> UserRooms { get; set; } = new HashSet<UserRoom>();
    }
}
