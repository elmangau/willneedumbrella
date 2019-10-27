using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.SqLite.Migrations
{
    public partial class GroupsUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "secgroup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secgroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "secuser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: false),
                    Recover = table.Column<bool>(nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secuser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "secgroupuser",
                columns: table => new
                {
                    GroupId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secgroupuser", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_secgroupuser_secgroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "secgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_secgroupuser_secuser_UserId",
                        column: x => x.UserId,
                        principalTable: "secuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_secgroup_Name",
                table: "secgroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_secgroupuser_UserId",
                table: "secgroupuser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_secuser_FirstName",
                table: "secuser",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_secuser_LastName",
                table: "secuser",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_secuser_UserName",
                table: "secuser",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "secgroupuser");

            migrationBuilder.DropTable(
                name: "secgroup");

            migrationBuilder.DropTable(
                name: "secuser");
        }
    }
}
