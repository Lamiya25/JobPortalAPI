using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortalAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class casca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Categories_CategoryID",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Locations_LocationID",
                table: "JobPosts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "609606b9-6e74-4169-8315-1ae3667a05ea");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9db70841-b4a9-42f6-b423-0acbd13ec5fd", "926c66b1-cc8b-4144-86ca-6b54b4838a4f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9db70841-b4a9-42f6-b423-0acbd13ec5fd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "926c66b1-cc8b-4144-86ca-6b54b4838a4f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "adf73adb-1cd6-458e-83cf-7f06211f1451", null, "Admin", "ADMIN" },
                    { "d9965d91-3d0a-4d93-bc60-0853201b0804", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpires", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "180e5606-cc70-413e-b505-95450085835d", 0, new DateTime(2024, 2, 11, 13, 33, 24, 949, DateTimeKind.Utc).AddTicks(2824), "8b5c660c-7cd5-49b3-8e24-94ccf482f8d7", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEIQnb91KSQcmp7a/REexTUNDh83nqGbfC5DI7MjO7Dr/gAQ/7PTwcpbeg8i9zmozKw==", null, false, null, null, "7d20d2ae-782e-41d0-8779-cf7ae947ae72", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "adf73adb-1cd6-458e-83cf-7f06211f1451", "180e5606-cc70-413e-b505-95450085835d", "AppUserRoles" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Categories_CategoryID",
                table: "JobPosts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Locations_LocationID",
                table: "JobPosts",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Categories_CategoryID",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Locations_LocationID",
                table: "JobPosts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9965d91-3d0a-4d93-bc60-0853201b0804");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "adf73adb-1cd6-458e-83cf-7f06211f1451", "180e5606-cc70-413e-b505-95450085835d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adf73adb-1cd6-458e-83cf-7f06211f1451");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "180e5606-cc70-413e-b505-95450085835d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "609606b9-6e74-4169-8315-1ae3667a05ea", null, "User", "USER" },
                    { "9db70841-b4a9-42f6-b423-0acbd13ec5fd", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpires", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "926c66b1-cc8b-4144-86ca-6b54b4838a4f", 0, new DateTime(2024, 2, 10, 15, 55, 12, 745, DateTimeKind.Utc).AddTicks(6992), "f53269dd-789c-4ec7-aaed-3a434dcc1709", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHcGJw62h/suloGNC5G31nebISk+MONkRPLgKMIPM4j0PNQh4CJUPjJ7XSQSHzR9vQ==", null, false, null, null, "e8a009a1-71bf-4ac4-85a4-fd470bbbce81", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "9db70841-b4a9-42f6-b423-0acbd13ec5fd", "926c66b1-cc8b-4144-86ca-6b54b4838a4f", "AppUserRoles" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Categories_CategoryID",
                table: "JobPosts",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Locations_LocationID",
                table: "JobPosts",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
