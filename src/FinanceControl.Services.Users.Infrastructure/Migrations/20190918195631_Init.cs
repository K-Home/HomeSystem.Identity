﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceControl.Services.Users.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Avatar_Name = table.Column<string>(maxLength: 500, nullable: true),
                    Avatar_Url = table.Column<string>(maxLength: 2000, nullable: true),
                    Avatar_IsEmpty = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: true),
                    LastName = table.Column<string>(maxLength: 150, nullable: true),
                    Email = table.Column<string>(maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: true),
                    Address_Street = table.Column<string>(maxLength: 180, nullable: true),
                    Address_City = table.Column<string>(maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(maxLength: 60, nullable: true),
                    Address_Country = table.Column<string>(maxLength: 90, nullable: true),
                    Address_ZipCode = table.Column<string>(maxLength: 18, nullable: true),
                    Password = table.Column<string>(maxLength: 1000, nullable: false),
                    Salt = table.Column<string>(maxLength: 500, nullable: false),
                    Role = table.Column<string>(nullable: false),
                    State = table.Column<string>(maxLength: 30, nullable: false),
                    TwoFactorAuthentication = table.Column<bool>("bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "OneTimeSecuredOperations",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(maxLength: 500, nullable: false),
                    RequesterIpAddress = table.Column<string>(maxLength: 50, nullable: false),
                    RequesterUserAgent = table.Column<string>(maxLength: 50, nullable: false),
                    ConsumerIpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    ConsumerUserAgent = table.Column<string>(maxLength: 50, nullable: true),
                    ConsumedAt = table.Column<DateTime>(nullable: true),
                    Expiry = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Consumed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeSecuredOperations", x => x.Id);
                    table.ForeignKey(
                        "FK_OneTimeSecuredOperations_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserSessions",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(maxLength: 1000, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 50, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Refreshed = table.Column<bool>("bit", nullable: false),
                    Destroyed = table.Column<bool>("bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        "FK_UserSessions_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_OneTimeSecuredOperations_UserId",
                "OneTimeSecuredOperations",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_UserSessions_UserId",
                "UserSessions",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OneTimeSecuredOperations");

            migrationBuilder.DropTable(
                "UserSessions");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}