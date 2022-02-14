﻿// <auto-generated />
using System;
using BRTF_Booking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BRTF_Booking.Data.BRFTMigrations
{
    [DbContext(typeof(BRFTContext))]
    partial class BRFTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21");

            modelBuilder.Entity("BRTF_Booking.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BookingRequested")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.HasIndex("UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTerm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AcadPlan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastLevel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StrtLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(7);

                    b.Property<int>("Term")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("ProgramTerms");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AreaID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Limit")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxNumofBookings")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rule")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BRTF_Booking.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentID")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(7);

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("StudentID")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Booking", b =>
                {
                    b.HasOne("BRTF_Booking.Models.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BRTF_Booking.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTerm", b =>
                {
                    b.HasOne("BRTF_Booking.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Booking.Models.Room", b =>
                {
                    b.HasOne("BRTF_Booking.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
