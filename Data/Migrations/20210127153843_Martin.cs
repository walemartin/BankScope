using Microsoft.EntityFrameworkCore.Migrations;

namespace CarScope.Data.Migrations
{
    public partial class Martin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankTransfer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    AccNo2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransfer", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransfer");
        }
    }
}
