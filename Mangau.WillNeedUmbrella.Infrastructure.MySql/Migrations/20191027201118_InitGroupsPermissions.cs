using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.MySql.Migrations
{
    public partial class InitGroupsPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "secpermissioncategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1L, "User Management Permissions", "Users" });

            migrationBuilder.InsertData(
                table: "secpermissioncategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2L, "System Management Permissions", "System" });

            migrationBuilder.InsertData(
                table: "secpermission",
                columns: new[] { "Id", "Description", "Name", "PermissionCategoryId" },
                values: new object[] { 1L, "The user can Login in the System", "Users.Login", 1L });

            migrationBuilder.InsertData(
                table: "secpermission",
                columns: new[] { "Id", "Description", "Name", "PermissionCategoryId" },
                values: new object[] { 2L, "The user can add other users to the System", "Users.AddUser", 1L });

            migrationBuilder.InsertData(
                table: "secgrouppermission",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.InsertData(
                table: "secgrouppermission",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[] { 2L, 1L });

            migrationBuilder.InsertData(
                table: "secgrouppermission",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[] { 2L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "secgrouppermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "secgrouppermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "secgrouppermission",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "secpermissioncategory",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "secpermission",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "secpermission",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "secpermissioncategory",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
