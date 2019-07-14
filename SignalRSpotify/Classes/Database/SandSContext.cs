using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SignalRSpotify.Classes.Database.Entities;
using System;

namespace SignalRSpotify.Classes.Database
{
    public class SandSContext : DbContext
    {
        public SandSContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoom>().HasKey("UserId", "RoomId");
            base.OnModelCreating(modelBuilder);
        }
        public EntityEntry Update(DbObject dbObjectEntity)
        {
            dbObjectEntity.LastOperationDate = DateTime.Now;
            return base.Update(dbObjectEntity);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<UserConnectionId> UserConnectionIds { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserRoom> UserRooms { get; set; }

    }
}
