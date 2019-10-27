using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.PostgreSQL.Migrations
{
    public partial class InitGroupsUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "secgroup",
                columns: new[] { "Id", "Active", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, true, "Everyone", "Everyone" },
                    { 2L, true, "System Administrators", "Administrators" }
                });

            migrationBuilder.InsertData(
                table: "secuser",
                columns: new[] { "Id", "Active", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { 1L, true, "System", "Administrator", "$2y$10$nLgfDdhTjYdUH6wbEctoLe0Ua6yjzx8YCksWZ/aaVGLpAb0hmtddG", "administrator" });

            migrationBuilder.InsertData(
                table: "secuser",
                columns: new[] { "Id", "Active", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { 2L, true, "Test", "01", "$2y$10$qrGKsfUDysr7fR18ZWlkxOYWMg6D.Of3CeCUzZLGC27xS4VV4AzqW", "test01" });

            migrationBuilder.InsertData(
                table: "secgroupuser",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 1L, 2L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "secgroupuser",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "secgroupuser",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "secgroupuser",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2L, 1L });

            migrationBuilder.DeleteData(
                table: "secgroup",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "secgroup",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "secuser",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "secuser",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
