using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.MySql.Migrations
{
    public partial class Cities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wnucity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Country = table.Column<string>(maxLength: 2, nullable: false),
                    State = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Coord_Lon = table.Column<float>(nullable: false),
                    Coord_Lat = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wnucity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wnucity_Country",
                table: "wnucity",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_wnucity_Name",
                table: "wnucity",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_wnucity_Country_Name",
                table: "wnucity",
                columns: new[] { "Country", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_wnucity_Country_State_Name",
                table: "wnucity",
                columns: new[] { "Country", "State", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wnucity");
        }
    }
}
