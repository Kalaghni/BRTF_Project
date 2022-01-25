﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRFTBooking.Models;

namespace BRFTBooking.Data
{
    public class BRFTContext : DbContext
    {
        public BRFTContext(DbContextOptions<BRFTContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BR");

            modelBuilder.Entity<User>()
                .HasMany<Booking>(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasMany<Booking>(u => u.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(a => a.StudentID)
                .IsUnique();
        }
    }
}
