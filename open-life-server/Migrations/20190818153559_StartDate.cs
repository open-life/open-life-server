using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace open_life_server.Migrations
{
    public partial class StartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "HabitGoals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "HabitGoals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "HabitGoals");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "HabitGoals");
        }
    }
}
