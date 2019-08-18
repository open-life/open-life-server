using Microsoft.EntityFrameworkCore.Migrations;

namespace open_life_server.Migrations
{
    public partial class ListName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ColumnName",
                table: "ListGoals",
                newName: "ListName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListName",
                table: "ListGoals",
                newName: "ColumnName");
        }
    }
}
