using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.migrations
{
    public partial class sdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_LName",
                table: "orders",
                newName: "ShippingAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_FName",
                table: "orders",
                newName: "ShippingAddress_FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress_LastName",
                table: "orders",
                newName: "ShippingAddress_LName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_FirstName",
                table: "orders",
                newName: "ShippingAddress_FName");
        }
    }
}
