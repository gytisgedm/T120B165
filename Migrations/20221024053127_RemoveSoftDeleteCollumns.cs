using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class RemoveSoftDeleteCollumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetEvents_FixedAssets_FixedAssetCode",
                table: "FixedAssetEvents");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "FixedAssets");

            migrationBuilder.DropColumn(
                name: "ContractTerminated",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "FixedAssetCode",
                table: "FixedAssetEvents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetEvents_FixedAssets_FixedAssetCode",
                table: "FixedAssetEvents",
                column: "FixedAssetCode",
                principalTable: "FixedAssets",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixedAssetEvents_FixedAssets_FixedAssetCode",
                table: "FixedAssetEvents");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "FixedAssets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "FixedAssetCode",
                table: "FixedAssetEvents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "ContractTerminated",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FixedAssetEvents_FixedAssets_FixedAssetCode",
                table: "FixedAssetEvents",
                column: "FixedAssetCode",
                principalTable: "FixedAssets",
                principalColumn: "Code");
        }
    }
}
