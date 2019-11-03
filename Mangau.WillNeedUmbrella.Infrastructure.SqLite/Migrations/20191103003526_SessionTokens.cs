﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mangau.WillNeedUmbrella.Infrastructure.SqLite.Migrations
{
    public partial class SessionTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "secsessiontoken",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(nullable: false),
                    LoggedAt = table.Column<DateTime>(nullable: false),
                    Expires = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secsessiontoken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secsessiontoken_secuser_UserId",
                        column: x => x.UserId,
                        principalTable: "secuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_secsessiontoken_UserId",
                table: "secsessiontoken",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "secsessiontoken");
        }
    }
}
