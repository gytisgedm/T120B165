using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class RemoveSurnameNameFromManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FixedAssetsManagers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "FixedAssetsManagers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FixedAssetsManagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "FixedAssetsManagers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
