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
                        Description = "This Suite Contains: Media Composer, Adobe Suite, DaVinci Resolve, Pro Tools",
                        Limit = 4,
                        Enabled = true,
                        
                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    }, 
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = true,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    },
                    new Room
                    {
                        Area = "Fred",
                        Name = "Reginald",
                        Description = "Flintstone",
                        Limit = 4,
                        Enabled = false,

                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
