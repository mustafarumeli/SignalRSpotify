using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Classes.Database.Entities
{
    public class UserConnectionId : DbObject
    {


        private User _user;
        [ForeignKey("Id")]
        public virtual User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        public string ConnectionId { get; set; }
    }


}
