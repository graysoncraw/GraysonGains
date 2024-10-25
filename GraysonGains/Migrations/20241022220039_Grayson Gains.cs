using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraysonGains.Migrations
{
    /// <inheritdoc />
    public partial class GraysonGains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HeightFeet = table.Column<int>(type: "int", nullable: false),
                    HeightInches = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserPRs",
                columns: table => new
                {
                    LiftPRId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BenchPR = table.Column<int>(type: "int", nullable: false),
                    SquatPR = table.Column<int>(type: "int", nullable: false),
                    DLPR = table.Column<int>(type: "int", nullable: false),
                    SPPR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPRs", x => x.LiftPRId);
                    table.ForeignKey(
                        name: "FK_UserPRs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkoutDays",
                columns: table => new
                {
                    WorkoutDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BenchDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SquatDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DlDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SPDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CycleStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutDays", x => x.WorkoutDayId);
                    table.ForeignKey(
                        name: "FK_UserWorkoutDays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutLogs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutWeight = table.Column<int>(type: "int", nullable: false),
                    WorkoutReps = table.Column<int>(type: "int", nullable: false),
                    WorkoutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_WorkoutLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPRs_UserId",
                table: "UserPRs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutDays_UserId",
                table: "UserWorkoutDays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_UserId",
                table: "WorkoutLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "UserPRs");

            migrationBuilder.DropTable(
                name: "UserWorkoutDays");

            migrationBuilder.DropTable(
                name: "WorkoutLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
