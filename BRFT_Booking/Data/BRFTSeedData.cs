using BRFT_Booking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Data
{
    public static class BRFTSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new BRFTContext(
                serviceProvider.GetRequiredService<DbContextOptions<BRFTContext>>()))
            {
                //Look for any Users(Students)
                if (!context.Users.Any())
                    {
                        context.Users.AddRange(
                         new User
                         {
                             ID = 4362670,
                             FName = "Cat",
                             MName = "Sun",
                             LName = "Al-Bahrini",
                             AcadPlan = "P0122",
                             Description = "Broadcasting: Radion, TV & Film",
                             Email = "cstevens@ncstudents.niagaracollege.ca",
                             StrtLevel = 01,
                             LastLevel = false,
                             Term = 1204
                         },

                         new User
                         {
                             ID = 9311011,
                             FName = "Sterling",
                             LName = "Archer",
                             AcadPlan = "P0163",
                             Description = "Presentation / Radio",
                             Email = "sarcher4@ncstudents.niagaracollege.ca",
                             StrtLevel = 01,
                             LastLevel = false,
                             Term = 1214
                         },
                         new User
                         {
                             ID = 9508725,
                             FName = "Matt",
                             MName = "Elmi-Johanna",
                             LName = "Parker",
                             AcadPlan = "P0164",
                             Description = "TV Production",
                             Email = "maparker1@ncstudents.niagaracollege.ca",
                             StrtLevel = 03,
                             LastLevel = false,
                             Term = 1214
                         },
                         new User
                         {
                             ID = 4407393,
                             FName = "James",
                             MName = "Bullough",
                             LName = "Lansing",
                             AcadPlan = "P0165",
                             Description = "Film Production",
                             Email = "jblansing@ncstudents.niagaracollege.ca",
                             StrtLevel = 05,
                             LastLevel = false,
                             Term = 1204
                         },
                         new User
                         {
                             ID = 4105233,
                             FName = "Judy",
                             MName = "Ugonna",
                             LName = "Garland",
                             AcadPlan = "P0198",
                             Description = "Acting for TV & Film",
                             Email = "jgarland@ncstudents.niagaracollege.ca",
                             StrtLevel = 02,
                             LastLevel = false,
                             Term = 1214
                         },
                         new User
                         {
                             ID = 4242885,
                             FName = "Marge",
                             LName = "Simpson",
                             AcadPlan = "P0795",
                             Description = "Digital Photography",
                             Email = "masimpson@ncstudents.niagaracollege.ca",
                             StrtLevel = 01,
                             LastLevel = false,
                             Term = 1214
                         },
                         new User
                         {
                             ID = 4035763,
                             FName = "Sarah",
                             MName = "Wing-Hay",
                             LName = "Slean",
                             AcadPlan = "P6801",
                             Description = "Joint BSc Game Programming",
                             Email = "sslean13@ncstudents.niagaracollege.ca",
                             StrtLevel = 04,
                             LastLevel = false,
                             Term = 1204
                         },
                         new User
                         {
                             ID = 4444312,
                             FName = "Morag",
                             MName = "Leah-Grace",
                             LName = "Smith",
                             AcadPlan = "P6800",
                             Description = "Joint BA Game Design",
                             Email = "msmith11@ncstudents.niagaracollege.ca",
                             StrtLevel = 06,
                             LastLevel = true,
                             Term = 1204
                         },
                         new User
                         {
                             ID = 4398123,
                             FName = "Akira",
                             MName = "Kaur",
                             LName = "Kurosawa",
                             AcadPlan = "P0441",
                             Description = "Game Development",
                             Email = "akurosawa@ncstudents.niagaracollege.ca",
                             StrtLevel = 03,
                             LastLevel = false,
                             Term = 1204
                         },
                         new User
                         {
                             ID = 9470695,
                             FName = "Zhuo-Chang",
                             LName = "Wu",
                             AcadPlan = "P0474",
                             Description = "CST – Network and Cloud Tech",
                             Email = "zcwu@ncstudents.niagaracollege.ca",
                             StrtLevel = 03,
                             LastLevel = false,
                             Term = 1214
                         }
                    );
                        context.SaveChanges();
                    }
                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(
                    new Room
                    {
                        Area = "Edit 13 BRTF1435 & 3Yr TV",
                        Name = "Edit 13",
                        Description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools.",
                        Rule = "Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time and have UP TO 3 future booking time-blocks",
                        Limit = 4,
                        MaxNumofBookings = 3,
                        Enabled = true,
                        
                    },
                    new Room
                    {
                        Area = "Edit 15 BRTF1435, Term 5",
                        Name = "Edit 15",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are bookable by 4th term Film/TV students, or 5th term TV students. You can book UP TO 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    }, 
                    new Room
                    {
                        Area = "Edit 6 3rd Year only",
                        Name = "Edit 6",
                        Description = "Media Composer, Pro Tools, Resolve and Creative Suite",
                        Rule = "You can book UP TO 6 hours at a time and UP TO 2 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edit 8 Inside Niagara",
                        Name = "Edit 8 V204i",
                        Description = "Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Bookable by 3rd term Presentation and 4th term TV students. You can book up to 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edit 9, 10 & 14 2nd Years",
                        Name = "Edit 9",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edit 9, 10 & 14 2nd Years",
                        Name = "Edit 10",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edit 9, 10 & 14 2nd Years",
                        Name = "Edit 14",
                        Description = "Media Composer, Pro Tools, DaVinci Resolve and Creative Suite",
                        Rule = "Suites are bookable by 2nd Year students and 3rd year Presentation students. You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edits 1-5 3rd Year Film",
                        Name = "Edit 1/2 Colour Suites",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Edits 1-5 3rd Year Film",
                        Name = "Edit 3",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Edits 1-5 3rd Year Film",
                        Name = "Edit 4",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Edits 1-5 3rd Year Film",
                        Name = "Edit 5",
                        Description = "Pro Tools, Media Composer, DaVinci Resolve, Creative Suite",
                        Rule = "Suites are restricted to 3RD YEAR FILM STUDENTS Only. All others will not be approved without a signed building pass. You can book UP TO 6 hours at a time and UP TO 3 future bookings.",
                        Limit = 6,
                        MaxNumofBookings = 3,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Film Studio V001",
                        Name = "Film Studio V001",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Green Room",
                        Name = "Green Room",
                        Description = "Ready Room typically for those that are preparing for a TV or Film shoot",
                        Rule = "Max bookable time is 12 hours",
                        Limit = 12,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 1",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 2",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 3",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 4",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 5",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 6",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 7",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 8",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 9",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 10",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 11",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 12",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 13",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 14",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 15",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 16",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "MAC Lab V106",
                        Name = "Computer 17",
                        Description = "All MACs Contain: MS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        Rule = "Max booking of 6 hours",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Mixing Theatre V105",
                        Name = "Mixing Theatre V5",
                        Description = "Special approval must be acquired from Luke Hutton before use.",
                        Rule = "Booking is only available after classes until midnight Monday to Friday. Weekends are OFF LIMITS. Maximum booking is 8 hours",
                        Limit = 8,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #1",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #2",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #3",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #4",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #5",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #6",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #7",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Edit Suites V109",
                        Name = "Audio Edit #8",
                        Rule = "Edit computers have a maximum booking time of 4 hours at a time.",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Studio & Talk A",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Studio B",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Studio C",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Studio D",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Announce Booth 1",
                        Description = "All Studios have phone access for interviews. Announce Booth 1 used for News and Sports",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Announce Booth 2",
                        Description = "All Studios have phone access for interviews. Announce Booth 2 used for Voice Tracking.",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #1",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #2",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #3",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #4",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #5",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #6",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #7",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Radio Recording Studios V109",
                        Name = "Audio Edit #8",
                        Description = "All Studios have phone access for interviews",
                        Rule = "You can book up to 2 hours in a studio",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "TV Studio V002",
                        Name = "V2 TV Studio",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V2 TV Studio, Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "TV Studio V002",
                        Name = "V2 GreenRoom",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V2 GreenRoom, Max Bookable Hours 6",
                        Limit = 6,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "TV Studio V002",
                        Name = "V1 (Old Studio)",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "V1 (Old Studio), Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "TV Studio V002",
                        Name = "TV Studio Control Room",
                        Description = "1st Year Students may reserve the Studio as per their Professor's instructions. ALL Others must obtain approval through Alysha Henderson.",
                        Rule = "TV Studio Control Room, Upstairs Control Room, Max Bookable Hours 2",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "V110",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "V110 Acting Lab",
                        Name = "Acting Lab V110",
                        Description = "Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "V110 Acting Lab",
                        Name = "V110g Acting Edit",
                        Description = "Booking is OFF LIMITS from 12:30am to the end of classes Mon-Fri. For exceptions, approval must be granted by Lori Ravensborg",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "V110f Acting Edit",
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "V204p Production Planning",
                        Name = "V204p Production Planning",
                        Description = "BRTF project meeting room. Booking is only available Mon-Friday between 8:30am to 5:30pm",
                        Rule = "You can book up to 1 hour",
                        Limit = 1,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Camera Test",
                        Name = "Red Camera 1",
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Edit 16 BRTF1435, Term 5 TV",
                        Name = "Edit 16 Avid/P2/DigLotPrt",
                        Description = "This Suites Contains: P2 Reader, Digitize/Log/Print Deck, SoundTrack, Avid, Final Cut Pro, DiffMerge, Adobe CS Suite, Aspera Connect",
                        Rule = "Suites are restricted to 4th TERM FILM/TV or 5th TERM TV STUDENTS ONLY. You can book UP TO 4 hours at a time.",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "MultiTrack V1j",
                        Name = "MultiTrack V1j",
                        Description = "This Suites Contains: Mixing Board, Attached Audio Booth, SoundTrack Pro. NOTE: Sountrack Pro is on all of the Edit Suites and MAC Lab",
                        Rule = "You can book UP TO 2 hours at a time.",
                        Limit = 2,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "V011 Assignment/Offload",
                        Name = "V011 Assignment/Offload",
                        Description = "Assignment finishing (as opposed to interrupting Mac lab classes) and footage offload before returning your camera media to the Equipment Room. Open Access space for finishing or media transfer.",
                        Rule = "Not bookable for meetings.",
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "V2 and S339 Acting",
                        Name = "V2 Acting",
                        Rule = "You can book UP TO 1 hour at a time.",
                        Limit = 1,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "V2 and S339 Acting",
                        Name = "S339",
                        Rule = "You can book UP TO 1 hour at a time.",
                        Limit = 1,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "V3 Demonstration Lab",
                        Name = "V3 Demonstration Lab",
                        Rule = "You can book UP TO 6 hours at a time.",
                        Limit = 6,
                        Enabled = false,

                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
