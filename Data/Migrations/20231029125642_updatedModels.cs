using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("1bca1678-10b8-404a-9e91-43e22b0d1e69"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("1f1b2c4b-a9fb-44e7-9509-9e684c10ef81"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("9132fe81-1fd1-4545-8a48-3c1d6a2ef693"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("956545fa-b4f2-4783-a112-adac2ff5533b"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("e229f65e-eb86-4807-9d9f-aec462d2f387"));

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("56f00e48-e65c-4444-b5d9-6aacd5a5fee4"), "Astrology" },
                    { new Guid("86040dc0-6853-452a-9c9e-ce1ce32e50bc"), "Physical education" },
                    { new Guid("cdf0b8d9-6fde-4520-9f60-14d48a882065"), "Math" },
                    { new Guid("f1824568-8480-49ea-94a9-36b3292219c0"), "Biology" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("56f00e48-e65c-4444-b5d9-6aacd5a5fee4"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("86040dc0-6853-452a-9c9e-ce1ce32e50bc"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("cdf0b8d9-6fde-4520-9f60-14d48a882065"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("f1824568-8480-49ea-94a9-36b3292219c0"));

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1bca1678-10b8-404a-9e91-43e22b0d1e69"), "Astrology" },
                    { new Guid("1f1b2c4b-a9fb-44e7-9509-9e684c10ef81"), "Math" },
                    { new Guid("9132fe81-1fd1-4545-8a48-3c1d6a2ef693"), "Biology" },
                    { new Guid("956545fa-b4f2-4783-a112-adac2ff5533b"), "Astrology" },
                    { new Guid("e229f65e-eb86-4807-9d9f-aec462d2f387"), "Physical education" }
                });
        }
    }
}
