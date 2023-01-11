using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchWebShop.Migrations
{
    public partial class DatenBankThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TaxRate",
                table: "OrderLines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "OrderLines");
        }
    }
}
