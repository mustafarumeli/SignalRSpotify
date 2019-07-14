﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignalRSpotify.Classes.Database;

namespace SignalRSpotify.Migrations
{
    [DbContext(typeof(SandSContext))]
    [Migration("20190712163412_test8.1")]
    partial class test81
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.Room", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastOperationDate");

                    b.Property<string>("RoomName");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastOperationDate");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.UserConnectionId", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConnectionId");

                    b.Property<DateTime>("LastOperationDate");

                    b.HasKey("Id");

                    b.ToTable("UserConnectionIds");
                });

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.UserRoom", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoomId");

                    b.HasKey("UserId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("UserRooms");
                });

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.UserConnectionId", b =>
                {
                    b.HasOne("SignalRSpotify.Classes.Database.Entities.User", "User")
                        .WithOne("UserConnectionId")
                        .HasForeignKey("SignalRSpotify.Classes.Database.Entities.UserConnectionId", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SignalRSpotify.Classes.Database.Entities.UserRoom", b =>
                {
                    b.HasOne("SignalRSpotify.Classes.Database.Entities.Room", "Room")
                        .WithMany("UserRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SignalRSpotify.Classes.Database.Entities.User", "User")
                        .WithMany("UserRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}