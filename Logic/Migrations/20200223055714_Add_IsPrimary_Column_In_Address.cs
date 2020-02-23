using Microsoft.EntityFrameworkCore.Migrations;

namespace Logic.Migrations
{
    public partial class Add_IsPrimary_Column_In_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Addresses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Addresses");
        }
    }
}
