using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinerBlazorServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Runtime = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfCustomers = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfWaiters = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfCooks = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfTables = table.Column<int>(type: "INTEGER", nullable: false),
                    AverageEntryTime = table.Column<int>(type: "INTEGER", nullable: false),
                    SetAveragePartySize = table.Column<int>(type: "INTEGER", nullable: false),
                    ActualAveragePartySize = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
