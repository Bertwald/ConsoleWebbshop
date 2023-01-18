using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWebbshopCodeFirst.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAdresses",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAdresses",
                table: "Customers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
