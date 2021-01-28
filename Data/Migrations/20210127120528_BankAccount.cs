using Microsoft.EntityFrameworkCore.Migrations;

namespace CarScope.Data.Migrations
{
    public partial class BankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    AccNo = table.Column<string>(nullable: true),
                    AvailableBal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BankDeposit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccNo = table.Column<string>(nullable: true),
                    Narration = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDeposit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BankWithDrawal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccNo = table.Column<string>(nullable: true),
                    Narration = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankWithDrawal", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankDeposit");

            migrationBuilder.DropTable(
                name: "BankWithDrawal");
        }
    }
}
