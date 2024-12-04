using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodosApi.Migrations
{
    /// <inheritdoc />
    public partial class NewTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 13, 5, 44, 971, DateTimeKind.Local).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 4, 13, 5, 44, 977, DateTimeKind.Local).AddTicks(286));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
