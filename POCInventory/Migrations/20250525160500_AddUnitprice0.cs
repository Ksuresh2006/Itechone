using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POCInventory.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitprice0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "inventory",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "inventory");
        }
    }
}
