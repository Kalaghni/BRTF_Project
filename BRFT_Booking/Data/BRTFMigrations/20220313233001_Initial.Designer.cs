﻿// <auto-generated />
using System;
using BRTF_Booking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BRTF_Booking.Data.BRTFMigrations
{
    [DbContext(typeof(BRTFContext))]
    [Migration("20220313233001_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21");

            modelBuilder.Entity("BRTF_Booking.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BookingRequested")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.HasIndex("UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("ProgramDetails");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTerm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AcadPlan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastLevel")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(1);

                    b.Property<int?>("ProgramDetailID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StrtLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Term")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProgramDetailID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("ProgramTerms");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTermGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProgramDetailID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProgramDetailID");

                    b.ToTable("ProgramTermGroups");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTermGroupArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AreaID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProgramTermGroupID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("AreaID");

                    b.HasIndex("ProgramTermGroupID");

                    b.ToTable("ProgramTermGroupAreas");
                });

            modelBuilder.Entity("BRTF_Booking.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApprovalEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApprovalName")
                        .HasColumnType("TEXT");

                    b.Property<int>("AreaID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasMaxLength(2000);

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Limit")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxNumofBookings")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
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

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("MName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProgramTermID")
                        .HasColumnType("INTEGER");

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
                    b.HasOne("BRTF_Booking.Models.ProgramDetail", "ProgramDetail")
                        .WithMany()
                        .HasForeignKey("ProgramDetailID");

                    b.HasOne("BRTF_Booking.Models.User", "User")
                        .WithOne("ProgramTerm")
                        .HasForeignKey("BRTF_Booking.Models.ProgramTerm", "UserID");
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTermGroup", b =>
                {
                    b.HasOne("BRTF_Booking.Models.ProgramDetail", "ProgramDetail")
                        .WithMany("ProgramTermGroups")
                        .HasForeignKey("ProgramDetailID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BRTF_Booking.Models.ProgramTermGroupArea", b =>
                {
                    b.HasOne("BRTF_Booking.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaID");

                    b.HasOne("BRTF_Booking.Models.ProgramTermGroup", "ProgramTermGroup")
                        .WithMany()
                        .HasForeignKey("ProgramTermGroupID");
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