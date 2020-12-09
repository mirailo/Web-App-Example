using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstRazorWebPage.Migrations
{
    public partial class StaffUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffUser",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffNo = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    StaffUserName = table.Column<string>(nullable: false),
                    StaffPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffUser", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffUser");
        }
    }
}
