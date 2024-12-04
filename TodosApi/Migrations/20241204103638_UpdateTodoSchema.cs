using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodosApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTodoSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 11, 35, 15, 610, DateTimeKind.Local).AddTicks(8845));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 11, 35, 15, 616, DateTimeKind.Local).AddTicks(1240));
        }
    }
}
