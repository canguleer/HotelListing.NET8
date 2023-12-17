using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class newPropAddedToCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b0c22cc-4f27-4c93-a7d8-4e7d5e1a8a76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9830b0fe-48e7-4cf0-964c-c99a0ef318ad");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChanged",
                table: "Country",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastChangedBy",
                table: "Country",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23115de4-b017-4942-a21c-043129d9a60c", null, "Administrator", "ADMINISTRATOR" },
                    { "8a5ff417-7a9c-4809-8143-9a8ead316f85", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "LastChanged", "LastChangedBy", "Name", "ShortName" },
                values: new object[,]
                {
                    { new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"), new DateTime(2023, 12, 16, 23, 58, 18, 982, DateTimeKind.Utc).AddTicks(7190), "ZZZ", "Cayman Island", "CI" },
                    { new Guid("1228d541-26e2-4b10-9560-29a9c92532b1"), new DateTime(2023, 12, 16, 23, 58, 18, 982, DateTimeKind.Utc).AddTicks(7188), "ZZZ", "Bahamas", "BS" },
                    { new Guid("68a16966-4bd7-4d69-8d6f-3464910a829f"), new DateTime(2023, 12, 16, 23, 58, 18, 982, DateTimeKind.Utc).AddTicks(7185), "ZZZ", "Jamaica", "JM" }
                });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "AdditionalInfo", "Address", "CountryId", "LastChanged", "LastChangedBy", "Name", "Rating" },
                values: new object[] { new Guid("fff56899-6925-4095-9da9-74a74d79c853"), null, "Negril", new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"), new DateTime(2023, 12, 16, 23, 58, 18, 982, DateTimeKind.Utc).AddTicks(7257), "ZZZ", "Sandals Resort and Spa", 4.2999999999999998 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23115de4-b017-4942-a21c-043129d9a60c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a5ff417-7a9c-4809-8143-9a8ead316f85");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("1228d541-26e2-4b10-9560-29a9c92532b1"));

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("68a16966-4bd7-4d69-8d6f-3464910a829f"));

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: new Guid("fff56899-6925-4095-9da9-74a74d79c853"));

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"));

            migrationBuilder.DropColumn(
                name: "LastChanged",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LastChangedBy",
                table: "Country");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b0c22cc-4f27-4c93-a7d8-4e7d5e1a8a76", null, "User", "USER" },
                    { "9830b0fe-48e7-4cf0-964c-c99a0ef318ad", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
