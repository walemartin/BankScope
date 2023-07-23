using Microsoft.EntityFrameworkCore.Migrations;

namespace CarScope.Data.Migrations
{
    public partial class InitialOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "ID", "AccNo", "AvailableBal", "Name" },
                values: new object[] { 1, "PL17234", 1000m, "Isaac Newton" });

            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "ID", "AccNo", "AvailableBal", "Name" },
                values: new object[] { 2, "PL13487", 1200m, "John Doe" });

            migrationBuilder.InsertData(
                table: "BankAccount",
                columns: new[] { "ID", "AccNo", "AvailableBal", "Name" },
                values: new object[] { 3, "PL19531", 600m, "Micheal Faraday" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
