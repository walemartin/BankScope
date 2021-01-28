using Microsoft.EntityFrameworkCore.Migrations;

namespace CarScope.Data.Migrations
{
    public partial class Cleopatra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterBankTransfer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccNo = table.Column<string>(nullable: true),
                    AvailableBal = table.Column<decimal>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    AccNo2 = table.Column<string>(nullable: true),
                    TransferAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterBankTransfer", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterBankTransfer");
        }
    }
}
