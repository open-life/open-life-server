using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace open_life_server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitGoals",
                columns: table => new
                {
                    HabitGoalId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitGoals", x => x.HabitGoalId);
                });

            migrationBuilder.CreateTable(
                name: "ListGoals",
                columns: table => new
                {
                    ListGoalId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Target = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListGoals", x => x.ListGoalId);
                });

            migrationBuilder.CreateTable(
                name: "NumberGoals",
                columns: table => new
                {
                    NumberGoalId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Target = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberGoals", x => x.NumberGoalId);
                });

            migrationBuilder.CreateTable(
                name: "HabitLogs",
                columns: table => new
                {
                    HabitLogId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    HabitCompleted = table.Column<bool>(nullable: false),
                    HabitGoalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitLogs", x => x.HabitLogId);
                    table.ForeignKey(
                        name: "FK_HabitLogs_HabitGoals_HabitGoalId",
                        column: x => x.HabitGoalId,
                        principalTable: "HabitGoals",
                        principalColumn: "HabitGoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    ListItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Progress = table.Column<int>(nullable: false),
                    ListGoalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.ListItemId);
                    table.ForeignKey(
                        name: "FK_ListItems_ListGoals_ListGoalId",
                        column: x => x.ListGoalId,
                        principalTable: "ListGoals",
                        principalColumn: "ListGoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberLogs",
                columns: table => new
                {
                    NumberLogId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    NumberGoalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberLogs", x => x.NumberLogId);
                    table.ForeignKey(
                        name: "FK_NumberLogs_NumberGoals_NumberGoalId",
                        column: x => x.NumberGoalId,
                        principalTable: "NumberGoals",
                        principalColumn: "NumberGoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_HabitGoalId",
                table: "HabitLogs",
                column: "HabitGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ListGoalId",
                table: "ListItems",
                column: "ListGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberLogs_NumberGoalId",
                table: "NumberLogs",
                column: "NumberGoalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitLogs");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "NumberLogs");

            migrationBuilder.DropTable(
                name: "HabitGoals");

            migrationBuilder.DropTable(
                name: "ListGoals");

            migrationBuilder.DropTable(
                name: "NumberGoals");
        }
    }
}
