using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BRTF_Booking.Data.BRTFMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    LName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SettingsViewModels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OfficeStartHours = table.Column<string>(nullable: true),
                    OfficeEndHours = table.Column<string>(nullable: true),
                    EmailExtension = table.Column<string>(nullable: true),
                    TermStart = table.Column<string>(nullable: true),
                    TermEnd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsViewModels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(maxLength: 7, nullable: false),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    MName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(maxLength: 100, nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    ProgramTermID = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AreaID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Rule = table.Column<string>(nullable: true),
                    Limit = table.Column<int>(nullable: true),
                    ApprovalName = table.Column<string>(nullable: true),
                    ApprovalEmail = table.Column<string>(nullable: true),
                    MaxNumofBookings = table.Column<int>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rooms_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTermGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ProgramDetailID = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTermGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramTermGroups_ProgramDetails_ProgramDetailID",
                        column: x => x.ProgramDetailID,
                        principalTable: "ProgramDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTerms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AcadPlan = table.Column<string>(nullable: false),
                    ProgramDetailID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    StrtLevel = table.Column<int>(nullable: false),
                    LastLevel = table.Column<string>(maxLength: 1, nullable: false),
                    Term = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTerms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramTerms_ProgramDetails_ProgramDetailID",
                        column: x => x.ProgramDetailID,
                        principalTable: "ProgramDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramTerms_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(nullable: false),
                    RoomID = table.Column<int>(nullable: false),
                    BookingRequested = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTermGroupAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramTermGroupID = table.Column<int>(nullable: true),
                    AreaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTermGroupAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramTermGroupAreas_Areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramTermGroupAreas_ProgramTermGroups_ProgramTermGroupID",
                        column: x => x.ProgramTermGroupID,
                        principalTable: "ProgramTermGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomID",
                table: "Bookings",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTermGroupAreas_AreaID",
                table: "ProgramTermGroupAreas",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTermGroupAreas_ProgramTermGroupID",
                table: "ProgramTermGroupAreas",
                column: "ProgramTermGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTermGroups_ProgramDetailID",
                table: "ProgramTermGroups",
                column: "ProgramDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTerms_ProgramDetailID",
                table: "ProgramTerms",
                column: "ProgramDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramTerms_UserID",
                table: "ProgramTerms",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AreaID",
                table: "Rooms",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentID",
                table: "Users",
                column: "StudentID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ProgramTermGroupAreas");

            migrationBuilder.DropTable(
                name: "ProgramTerms");

            migrationBuilder.DropTable(
                name: "SettingsViewModels");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "ProgramTermGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "ProgramDetails");
        }
    }
}
