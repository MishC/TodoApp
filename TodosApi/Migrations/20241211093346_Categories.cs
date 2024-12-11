using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodosApi.Migrations
{
    /// <inheritdoc />
    public partial class Categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CategoryDescription = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryDescription", "Name", "Priority" },
                values: new object[,]
                {
                    { 1, "Practical things which need to be done as first", "Personal", true },
                    { 2, "Spend time with loved ones", "Family", false },
                    { 3, "Stay fit and healthy", "Sport", false },
                    { 4, "Work-related tasks", "Job", true },
                    { 5, "Groceries and essentials", "Shopping", false },
                    { 6, "Take care of yourself", "Beauty & Wellness", false },
                    { 7, "What to do at home", "Home", false }
                });

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "CurrentDate" },
                values: new object[] { 3, new DateTime(2024, 12, 11, 10, 33, 45, 543, DateTimeKind.Local).AddTicks(381) });

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "CurrentDate", "Title" },
                values: new object[] { 1, new DateTime(2024, 12, 11, 10, 33, 45, 548, DateTimeKind.Local).AddTicks(4145), "Take kids from the school before 4.20pm" });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_CategoryId",
                table: "Todos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Categories_CategoryId",
                table: "Todos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Categories_CategoryId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Todos_CategoryId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Todos");

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentDate",
                value: new DateTime(2024, 12, 10, 10, 48, 10, 582, DateTimeKind.Local).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CurrentDate", "Title" },
                values: new object[] { new DateTime(2024, 12, 10, 10, 48, 10, 588, DateTimeKind.Local).AddTicks(1139), "Take kids from the school before 4pm" });
        }
    }
}
