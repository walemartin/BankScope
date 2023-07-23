using Microsoft.EntityFrameworkCore.Migrations;

namespace CarScope.Data.Migrations
{
    public partial class InitialOne1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 1,
                column: "AccNo",
                value: "PL14429");

            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 2,
                column: "AccNo",
                value: "PL17271");

            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 3,
                column: "AccNo",
                value: "PL17471");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 1,
                column: "AccNo",
                value: "PL17234");

            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 2,
                column: "AccNo",
                value: "PL13487");

            migrationBuilder.UpdateData(
                table: "BankAccount",
                keyColumn: "ID",
                keyValue: 3,
                column: "AccNo",
                value: "PL19531");
        }
    }
}
