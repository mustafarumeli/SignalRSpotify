using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSpotify.Classes.Database.Entities
{
    public abstract class DbObject
    {
        public string Id { get; set; }
        public DateTime LastOperationDate { get; set; }
        public DbObject()
        {
            Id = Guid.NewGuid().ToString();
            LastOperationDate = DateTime.Now;
        }
    }
}
