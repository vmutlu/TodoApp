using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.DataAccess.Migrations
{
    public partial class database_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Todos",
                newName: "IsFavorite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFavorite",
                table: "Todos",
                newName: "Title");
        }
    }
}
