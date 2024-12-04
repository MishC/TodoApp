using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodosApi.Migrations
{
    /// <inheritdoc />
    public partial class newController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 5, 0, 12, 14, 357, DateTimeKind.Local).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 5, 0, 12, 14, 362, DateTimeKind.Local).AddTicks(6469));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 13, 24, 34, 622, DateTimeKind.Local).AddTicks(5975));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 13, 24, 34, 627, DateTimeKind.Local).AddTicks(8153));
        }
    }
}
