using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullableTrachChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastChangedBy",
                table: "Hotel",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                collation: "Latin1_General_CI_AS",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldCollation: "Latin1_General_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Hotel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("158c3c5d-063b-4531-8553-6fc5e5a8c7c2"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2635), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2632) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f13b959-78dc-47b4-b714-7ecbc862e0da"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2639), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2638) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2787), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("1228d541-26e2-4b10-9560-29a9c92532b1"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2785), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2785) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("68a16966-4bd7-4d69-8d6f-3464910a829f"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2782), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2782) });

            migrationBuilder.UpdateData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: new Guid("fff56899-6925-4095-9da9-74a74d79c853"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2851), new DateTime(2023, 12, 18, 20, 18, 17, 993, DateTimeKind.Utc).AddTicks(2850) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastChangedBy",
                table: "Hotel",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                collation: "Latin1_General_CI_AS",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true,
                oldCollation: "Latin1_General_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Hotel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("158c3c5d-063b-4531-8553-6fc5e5a8c7c2"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(6881), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(6879) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f13b959-78dc-47b4-b714-7ecbc862e0da"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(6884), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(6883) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7022), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7022) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("1228d541-26e2-4b10-9560-29a9c92532b1"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7020), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: new Guid("68a16966-4bd7-4d69-8d6f-3464910a829f"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7017), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7017) });

            migrationBuilder.UpdateData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: new Guid("fff56899-6925-4095-9da9-74a74d79c853"),
                columns: new[] { "CreatedOn", "LastChangedOn" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7088), new DateTime(2023, 12, 18, 20, 9, 49, 570, DateTimeKind.Utc).AddTicks(7087) });
        }
    }
}
