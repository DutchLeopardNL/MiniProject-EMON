using Microsoft.EntityFrameworkCore.Migrations;

namespace EMONAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datagrams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    timeStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currentUsage = table.Column<double>(type: "float", nullable: false),
                    totalLow = table.Column<double>(type: "float", nullable: false),
                    totalHigh = table.Column<double>(type: "float", nullable: false),
                    returnLow = table.Column<double>(type: "float", nullable: false),
                    returnHigh = table.Column<double>(type: "float", nullable: false),
                    gasUsage = table.Column<double>(type: "float", nullable: false),
                    signature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datagrams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datagrams");
        }
    }
}
