using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstRazorWebPage.Migrations
{
    public partial class Services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "UserAccessData");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceCode = table.Column<string>(nullable: false),
                    ServiceName = table.Column<string>(nullable: false),
                    ServiceDuration = table.Column<int>(nullable: false),
                    ServicePrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Level = table.Column<string>(type: "TEXT", nullable: false),
                    MCode = table.Column<string>(type: "TEXT", nullable: false),
                    MName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAdd = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    Phrase = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessData", x => x.ID);
                });
        }
    }
}
