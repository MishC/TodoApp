using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodosApi.Migrations
{
    /// <inheritdoc />
    public partial class configureWarns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 12, 0, 3, 673, DateTimeKind.Local).AddTicks(4596));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 12, 0, 3, 678, DateTimeKind.Local).AddTicks(8150));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 11, 36, 38, 110, DateTimeKind.Local).AddTicks(2607));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 11, 36, 38, 115, DateTimeKind.Local).AddTicks(7877));
        }
    }
}
