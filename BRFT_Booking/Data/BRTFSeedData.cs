using BRTF_Booking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Data
{
    public static class BRTFSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BRTFContext(
                serviceProvider.GetRequiredService<DbContextOptions<BRTFContext>>()))
            {
                if (!context.ProgramDetails.Any())
                {
                    context.ProgramDetails.AddRange(
                        new ProgramDetail
                        {
                            Name = "Broadcasting: Radio, TV & Film"
                        },
                        new ProgramDetail
                        {
                            Name = "Presentation / Radio"
                        },
                        new ProgramDetail
                        {
                            Name = "TV Production"
                        },
                        new ProgramDetail
                        {
                            Name = "Film Production"
                        },
                        new ProgramDetail
                        {
                            Name = "Acting for TV & Film"
                        },
                        new ProgramDetail
                        {
                            Name = "Digital Photography",
                        },
                        new ProgramDetail
                        {
                            Name = "Joint BSc Game Programming",
                        },
                        new ProgramDetail
                        {
                            Name = "Joint BA Game Design",
                        },
                        new ProgramDetail
                        {
                            Name = "Game Development",
                        },
                        new ProgramDetail
                        {
                            Name = "CST – Network and Cloud Tech",
                        }
                        );
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                     new User
                     {
                         StudentID = "4362670",
                         FName = "Cat",
                         MName = "Sun",
                         LName = "Al-Bahrini",
                         Email = "cstevens@ncstudents.niagaracollege.ca"
                     },

                     new User
                     {
                         StudentID = "9311011",
                         FName = "Sterling",
                         LName = "Archer",
                         Email = "sarcher4@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "9508725",
                         FName = "Matt",
                         MName = "Elmi-Johanna",
                         LName = "Parker",
                         Email = "maparker1@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4407393",
                         FName = "James",
                         MName = "Bullough",
                         LName = "Lansing",
                         Email = "jblansing@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4105233",
                         FName = "Judy",
                         MName = "Ugonna",
                         LName = "Garland",
                         Email = "jgarland@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4242885",
                         FName = "Marge",
                         LName = "Simpson",
                         Email = "masimpson@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4035763",
                         FName = "Sarah",
                         MName = "Wing-Hay",
                         LName = "Slean",
                         Email = "sslean13@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4444312",
                         FName = "Morag",
                         MName = "Leah-Grace",
                         LName = "Smith",
                         Email = "msmith11@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "4398123",
                         FName = "Akira",
                         MName = "Kaur",
                         LName = "Kurosawa",
                         Email = "akurosawa@ncstudents.niagaracollege.ca"
                     },
                     new User
                     {
                         StudentID = "9470695",
                         FName = "Zhuo-Chang",
                         LName = "Wu",
                         Email = "zcwu@ncstudents.niagaracollege.ca"
                     }
                );
                    context.SaveChanges();
                }

                if (!context.ProgramTerms.Any())
                {
                    context.ProgramTerms.AddRange(
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "cstevens@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0122",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Broadcasting: Radio, TV & Film").FirstOrDefault(),
                            StrtLevel = 01,
                            LastLevel = "N",
                            Term = 1204
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "sarcher4@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0163",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            StrtLevel = 01,
                            LastLevel = "N",
                            Term = 1214
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "maparker1@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0164",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "TV Production").FirstOrDefault(),
                            StrtLevel = 03,
                            LastLevel = "N",
                            Term = 1214
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "jblansing@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0165",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Film Production").FirstOrDefault(),
                            StrtLevel = 05,
                            LastLevel = "N",
                            Term = 1204
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "jgarland@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0198",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Acting for TV & Film").FirstOrDefault(),
                            StrtLevel = 02,
                            LastLevel = "N",
                            Term = 1214
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "masimpson@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0795",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Digital Photography").FirstOrDefault(),
                            StrtLevel = 01,
                            LastLevel = "N",
                            Term = 1214
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "sslean13@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P6801",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Joint BSc Game Programming").FirstOrDefault(),
                            StrtLevel = 04,
                            LastLevel = "N",
                            Term = 1204
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "msmith11@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P6800",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Joint BA Game Design").FirstOrDefault(),
                            StrtLevel = 06,
                            LastLevel = "Y",
                            Term = 1204
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "akurosawa@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0441",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Game Development").FirstOrDefault(),
                            StrtLevel = 03,
                            LastLevel = "N",
                            Term = 1204
                        },
                        new ProgramTerm
                        {
                            User = context.Users.Where(u => u.Email == "zcwu@ncstudents.niagaracollege.ca").FirstOrDefault(),
                            AcadPlan = "P0474",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "CST – Network and Cloud Tech").FirstOrDefault(),
                            StrtLevel = 03,
                            LastLevel = "N",
                            Term = 1214
                        });

                    context.SaveChanges();
                    foreach (ProgramTerm program in context.ProgramTerms)
                    {
                        var userToUpdate = context.Users.Where(u => program.UserID == u.ID).FirstOrDefault();
                        userToUpdate.ProgramTermID = program.ID;
                        context.Update(userToUpdate);
                    }
                    context.SaveChanges();
                }

                if (!context.Areas.Any())
                {
                    context.Areas.AddRange(
                        new Area
                        {
                            Name = "Edit 13 BRTF1435 & 3Yr TV"
                        },
                        new Area
                        {
                            Name = "Edit 15 BRTF1435, Term 5"
                        },
                        new Area
                        {
                            Name = "Edit 6 3rd Year only"
                        },
                        new Area
                        {
                            Name = "Edit 8 Inside Niagara"
                        },
                        new Area
                        {
                            Name = "Edit 9, 10 & 14 2nd Years"
                        },
                        new Area
                        {
                            Name = "Edits 1-5 3rd Year Film"
                        },
                        new Area
                        {
                            Name = "Film Studio V001"
                        },
                        new Area
                        {
                            Name = "Green Room"
                        },
                        new Area
                        {
                            Name = "MAC Lab V106"
                        },
                        new Area
                        {
                            Name = "Mixing Theatre V105"
                        },
                        new Area
                        {
                            Name = "Radio Edit Suites V109"
                        },
                        new Area
                        {
                            Name = "Radio Recording Studios V109"
                        },
                        new Area
                        {
                            Name = "TV Studio V002"
                        },
                        new Area
                        {
                            Name = "V110"
                        },
                        new Area
                        {
                            Name = "V110 Acting Lab"
                        },
                        new Area
                        {
                            Name = "V110f Acting Edit"
                        },
                        new Area
                        {
                            Name = "V204p Production Planning"
                        },
                        new Area
                        {
                            Name = "Camera Test"
                        },
                        new Area
                        {
                            Name = "Edit 16 BRTF1435, Term 5 TV"
                        },
                        new Area
                        {
                            Name = "MultiTrack V1j"
                        },
                        new Area
                        {
                            Name = "V011 Assignment/Offload"
                        },
                        new Area
                        {
                            Name = "V2 and S339 Acting"
                        },
                        new Area
                        {
                            Name = "V3 Demonstration Lab"
                        });
                    context.SaveChanges();
                }

                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 13 BRTF1435 & 3Yr TV").FirstOrDefault(),
                        Name = "Edit 13",
                        Description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools.",
                        Rule = "Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time and have UP TO 3 future booking time-blocks",
                        Limit = 4,
                        MaxNumofBookings = 3,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 15 BRTF1435, Term 5").FirstOrDefault(),
                        Name = "Edit 15",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are bookable by 4th term Film/TV students, or 5th term TV students. You can book UP TO 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 6 3rd Year only").FirstOrDefault(),
                        Name = "Edit 6",
                        Description = "Media Composer, Pro Tools, Resolve and Creative Suite",
                        Rule = "You can book UP TO 6 hours at a time and UP TO 2 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 8 Inside Niagara").FirstOrDefault(),
                        Name = "Edit 8 V204i",
                        Description = "Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Bookable by 3rd term Presentation and 4th term TV students. You can book up to 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 9, 10 & 14 2nd Years").FirstOrDefault(),
                        Name = "Edit 9",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 9, 10 & 14 2nd Years").FirstOrDefault(),
                        Name = "Edit 10",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 9, 10 & 14 2nd Years").FirstOrDefault(),
                        Name = "Edit 14",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edits 1-5 3rd Year Film").FirstOrDefault(),
                        Name = "Edit 1/2 Colour Suites",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edits 1-5 3rd Year Film").FirstOrDefault(),
                        Name = "Edit 3",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edits 1-5 3rd Year Film").FirstOrDefault(),
                        Name = "Edit 4",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edits 1-5 3rd Year Film").FirstOrDefault(),
                        Name = "Edit 5",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Film Studio V001").FirstOrDefault(),
                        Name = "Film Studio V001",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Green Room").FirstOrDefault(),
                        Name = "Green Room",
                        Description = "Ready Room typically for those that are preparing for a TV or Film shoot",
                        Rule = "Max bookable time is 12 hours",
                        Limit = 12,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 1",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 2",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 3",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 4",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 5",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 6",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 7",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 8",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 9",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 10",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 11",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 12",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 13",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 14",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 15",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 16",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MAC Lab V106").FirstOrDefault(),
                        Name = "Computer 17",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Mixing Theatre V105").FirstOrDefault(),
                        Name = "Mixing Theatre V5",
                        Description = "Special approval must be acquired from Luke Hutton before use.",
                        Rule = "Booking is only available after classes until midnight Monday to Friday. Weekends are OFF LIMITS. Maximum booking is 8 hours",
                        Limit = 8,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #1",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #2",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #3",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #4",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #5",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #6",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #7",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Edit Suites V109").FirstOrDefault(),
                        Name = "Audio Edit #8",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Studio & Talk A",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Studio B",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Studio C",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Studio D",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Announce Booth 1",
                        Description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Announce Booth 2",
                        Description = "All Studios have phone access for interviews. Announce Booth 2 used for Voice Tracking.",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #1",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #2",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #3",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #4",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #5",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #6",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #7",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Radio Recording Studios V109").FirstOrDefault(),
                        Name = "Audio Edit #8",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "TV Studio V002").FirstOrDefault(),
                        Name = "V2 TV Studio",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V2 TV Studio, Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "TV Studio V002").FirstOrDefault(),
                        Name = "V2 GreenRoom",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V2 GreenRoom, Max Bookable Hours 6",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "TV Studio V002").FirstOrDefault(),
                        Name = "V1 (Old Studio)",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V1 (Old Studio), Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "TV Studio V002").FirstOrDefault(),
                        Name = "TV Studio Control Room",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "TV Studio Control Room, Upstairs Control Room, Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V110").FirstOrDefault(),
                        Name = "V110",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V110 Acting Lab").FirstOrDefault(),
                        Name = "Acting Lab V110",
                        Description = "Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V110 Acting Lab").FirstOrDefault(),
                        Name = "V110g Acting Edit",
                        Description = "Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V110f Acting Edit").FirstOrDefault(),
                        Name = "V110f Acting Edit",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V204p Production Planning").FirstOrDefault(),
                        Name = "V204p Production Planning",
                        Description = "BRTF project meeting room. Booking is only available Mon-Friday between 8:30am to 5:30pm",
                        Rule = "You can book up to 1 hour",
                        Limit = 1,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Camera Test").FirstOrDefault(),
                        Name = "Red Camera 1",
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "Edit 16 BRTF1435, Term 5 TV").FirstOrDefault(),
                        Name = "Edit 16 Avid/P2/DigLotPrt",
                        Description = "This Suites Contains: P2 Reader, Digitize/Log/Print Deck, SoundTrack, Avid, Final Cut Pro, DiffMerge, Adobe CS Suite, Aspera Connect",
                        Rule = "Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time.",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "MultiTrack V1j").FirstOrDefault(),
                        Name = "MultiTrack V1j",
                        Description = "This Suites Contains: Mixing Board, Attached Audio Booth, SoundTrack Pro. NOTE: Sountrack Pro is on all of the Edit Suites and MAC Lab",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V011 Assignment/Offload").FirstOrDefault(),
                        Name = "V011 Assignment/Offload",
                        Description = "Assignment finishing (as opposed to interrupting Mac lab classes) and footage offload before returning your camera media to the Equipment Room. Open Access space for finishing or media transfer.",
                        Rule = "Not bookable for meetings.",
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V2 and S339 Acting").FirstOrDefault(),
                        Name = "V2 Acting",
                        Rule = "You can book UP TO 1 hour at a time.",
                        Limit = 1,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V2 and S339 Acting").FirstOrDefault(),
                        Name = "S339",
                        Rule = "You can book UP TO 1 hour at a time.",
                        Limit = 1,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = context.Areas.Where(a => a.Name == "V3 Demonstration Lab").FirstOrDefault(),
                        Name = "V3 Demonstration Lab",
                        Rule = "You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = false,

                    }
                    );
                    context.SaveChanges();
                }

                if (!context.ProgramTermGroups.Any())
                {
                    context.ProgramTermGroups.AddRange(                      
                        new ProgramTermGroup
                        {
                            Name = "combo1",
                            Level = 1
                        },
                        new ProgramTermGroup
                        {
                            Name = "combo2",
                            Level = 2
                        },
                        new ProgramTermGroup
                        {
                            Name = "tv3",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "TV Production").FirstOrDefault(),
                            Level = 3
                        },
                        new ProgramTermGroup
                        {
                            Name = "tv4",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "TV Production").FirstOrDefault(),
                            Level = 4
                        },
                        new ProgramTermGroup
                        {
                            Name = "tv5",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "TV Production").FirstOrDefault(),
                            Level = 5
                        },
                        new ProgramTermGroup
                        {
                            Name = "tv6",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "TV Production").FirstOrDefault(),
                            Level = 6
                        },
                        new ProgramTermGroup
                        {
                            Name = "film3",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Film Production").FirstOrDefault(),
                            Level = 3
                        },
                        new ProgramTermGroup
                        {
                            Name = "film4",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Film Production").FirstOrDefault(),
                            Level = 4
                        },
                        new ProgramTermGroup
                        {
                            Name = "film5",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Film Production").FirstOrDefault(),
                            Level = 5
                        },
                        new ProgramTermGroup
                        {
                            Name = "film6",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Film Production").FirstOrDefault(),
                            Level = 6
                        },
                        new ProgramTermGroup
                        {
                            Name = "pres2",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            Level = 2
                        },
                        new ProgramTermGroup
                        {
                            Name = "pres3",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            Level = 3
                        },
                        new ProgramTermGroup
                        {
                            Name = "pres4",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            Level = 4
                        },
                        new ProgramTermGroup
                        {
                            Name = "pres5",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            Level = 5
                        },
                        new ProgramTermGroup
                        {
                            Name = "pres6",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Presentation / Radio").FirstOrDefault(),
                            Level = 6
                        },
                        new ProgramTermGroup
                        {
                            Name = "acting",
                            ProgramDetail = context.ProgramDetails.Where(u => u.Name == "Acting for TV & Film").FirstOrDefault(),
                            Level = 0
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
        public static void InitializeAdmins(IServiceProvider serviceProvider)
        {

            using (var context = new BRTFContext(
                serviceProvider.GetRequiredService<DbContextOptions<BRTFContext>>()))
            {
                if (!context.Admins.Any())
                {
                    context.Admins.AddRange(
                        new Admin
                        {
                            FName = "Simone",
                            LName = "Smith",
                            Email = "ssmith@niagaracollege.ca",
                            Role = "Admin"
                        },
                        new Admin
                        {
                            FName = "Byron",
                            LName = "Gracey",
                            Email = "bgracey@niagaracollege.ca",
                            Role = "Top-Level Admin"
                        },
                        new Admin
                        {
                            FName = "Bruce",
                            LName = "Ashford",
                            Email = "Bashford@niagaracollege.ca",
                            Role = "Admin"
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
