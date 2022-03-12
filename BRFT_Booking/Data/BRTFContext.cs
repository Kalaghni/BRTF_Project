using BRTF_Booking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Data
{
    public class BRTFContext : DbContext
    {
        public BRTFContext(DbContextOptions<BRTFContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProgramTerm> ProgramTerms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProgramTermGroup> ProgramTermGroups { get; set; }
        public DbSet<ProgramDetail> ProgramDetails { get; set; }
        public DbSet<ProgramTermGroupArea> ProgramTermGroupAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("BR");

            //Prevent Cascade Delete
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

            modelBuilder.Entity<ProgramDetail>()
                .HasMany<ProgramTermGroup>(u => u.ProgramTermGroups)
                .WithOne(b => b.ProgramDetail)
                .HasForeignKey(b => b.ProgramDetailID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index
            modelBuilder.Entity<User>()
                .HasIndex(a => a.StudentID)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(a => new { a.Email })
                .IsUnique();

            modelBuilder.Entity<Admin>().HasIndex(a => a.Email).IsUnique();

            //Foreign key constraints
            modelBuilder.Entity<ProgramTerm>()
                .HasOne(u => u.User)
                .WithOne(b => b.ProgramTerm)
                .HasForeignKey<User>(b => b.ProgramTermID);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ProgramTerm)
                .WithOne(b => b.User)
                .HasForeignKey<ProgramTerm>(b => b.UserID);

           /* modelBuilder.Entity<ProgramTermGroupArea>()
               .HasOne(u => u.Area)
               .WithOne(b => b.ProgramTermGroupArea)
               .HasForeignKey<Area>(b => b.ProgramTermGroupAreaID);

            modelBuilder.Entity<ProgramTermGroupArea>()
                .HasOne(u => u.ProgramTermGroup)
                .WithOne(b => b.ProgramTermGroupArea)
                .HasForeignKey<ProgramTermGroup>(b => b.ProgramTermGroupAreaID);

            modelBuilder.Entity<Area>()
                .HasOne(u => u.ProgramTermGroupArea)
                .WithOne(b => b.Area)
                .HasForeignKey<ProgramTermGroupArea>(b => b.AreaID);

            modelBuilder.Entity<ProgramTermGroup>()
               .HasOne(u => u.ProgramTermGroupArea)
               .WithOne(b => b.ProgramTermGroup)
               .HasForeignKey<ProgramTermGroupArea>(b => b.ProgramTermGroupID);*/

            
        }
    }
}
