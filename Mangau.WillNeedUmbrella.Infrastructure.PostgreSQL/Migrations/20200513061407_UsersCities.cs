using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.PostgreSQL.Migrations
{
    public partial class UsersCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "secuser",
                maxLength: 128,
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "wnuusercity",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wnuusercity", x => new { x.UserId, x.CityId });
                    table.ForeignKey(
                        name: "FK_wnuusercity_wnucity_CityId",
                        column: x => x.CityId,
                        principalTable: "wnucity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wnuusercity_secuser_UserId",
                        column: x => x.UserId,
                        principalTable: "secuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wnuusercity_CityId",
                table: "wnuusercity",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wnuusercity");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "secuser");
        }
    }
}
