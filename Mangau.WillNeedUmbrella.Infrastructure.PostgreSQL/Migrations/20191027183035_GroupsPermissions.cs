using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mangau.WillNeedUmbrella.Infrastructure.PostgreSQL.Migrations
{
    public partial class GroupsPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "secpermissioncategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secpermissioncategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "secpermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    PermissionCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secpermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secpermission_secpermissioncategory_PermissionCategoryId",
                        column: x => x.PermissionCategoryId,
                        principalTable: "secpermissioncategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "secgrouppermission",
                columns: table => new
                {
                    GroupId = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secgrouppermission", x => new { x.GroupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_secgrouppermission_secgroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "secgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_secgrouppermission_secpermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "secpermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_secgrouppermission_PermissionId",
                table: "secgrouppermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_secpermission_Name",
                table: "secpermission",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_secpermission_PermissionCategoryId",
                table: "secpermission",
                column: "PermissionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_secpermissioncategory_Name",
                table: "secpermissioncategory",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "secgrouppermission");

            migrationBuilder.DropTable(
                name: "secpermission");

            migrationBuilder.DropTable(
                name: "secpermissioncategory");
        }
    }
}
