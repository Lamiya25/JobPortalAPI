using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortalAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7cafaaf-8b79-434b-b44d-ac937e573a5c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "421262f3-2e97-4499-83ea-1bc965f04d14", "dd916a9f-43e0-4a46-bdbb-02a868e254e5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421262f3-2e97-4499-83ea-1bc965f04d14");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dd916a9f-43e0-4a46-bdbb-02a868e254e5");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Storage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseFiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24a6d196-5978-4bd8-849d-0e42864d1711", null, "User", "USER" },
                    { "692502b8-90a0-482a-8da2-d2ac11179535", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageId", "RefreshToken", "RefreshTokenExpires", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "290e26ae-d433-4fbe-8bf5-46d801ed7975", 0, new DateTime(2024, 2, 19, 20, 11, 49, 504, DateTimeKind.Utc).AddTicks(6546), "0ea0aae0-62ca-43e3-b1db-c7d512f4121e", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEGE7+QIl3dg4kajumU2MnVgM/SZfl7GrLR0CrXnyNKjSR74gM9Pv9fds9Lq3TG8kYw==", null, false, null, null, null, "05d94726-b3cb-46e8-b079-3a17f1b5cfe2", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "692502b8-90a0-482a-8da2-d2ac11179535", "290e26ae-d433-4fbe-8bf5-46d801ed7975", "AppUserRoles" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BaseFiles_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "BaseFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BaseFiles_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BaseFiles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24a6d196-5978-4bd8-849d-0e42864d1711");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "692502b8-90a0-482a-8da2-d2ac11179535", "290e26ae-d433-4fbe-8bf5-46d801ed7975" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "692502b8-90a0-482a-8da2-d2ac11179535");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "290e26ae-d433-4fbe-8bf5-46d801ed7975");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421262f3-2e97-4499-83ea-1bc965f04d14", null, "Admin", "ADMIN" },
                    { "e7cafaaf-8b79-434b-b44d-ac937e573a5c", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpires", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dd916a9f-43e0-4a46-bdbb-02a868e254e5", 0, new DateTime(2024, 2, 19, 17, 11, 10, 230, DateTimeKind.Utc).AddTicks(9397), "3abc3d70-8999-41bf-9c7a-d2c44447a8a2", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHvEnDRqD89ui83DsEGh+OtoQjxaz/41UbqHHOi9wL5RiEgMkT9hJ4k4i42ycXMSpQ==", null, false, null, null, "0b37ded8-d0c5-4db9-80c5-74566645e45e", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "421262f3-2e97-4499-83ea-1bc965f04d14", "dd916a9f-43e0-4a46-bdbb-02a868e254e5", "AppUserRoles" });
        }
    }
}
