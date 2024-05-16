using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB_TH_API.Migrations
{
    /// <inheritdoc />
    public partial class ImagesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1951), new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1936) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1954), new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1961), new DateTime(2024, 5, 14, 8, 3, 12, 531, DateTimeKind.Local).AddTicks(1960) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6176), new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6163) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6180), new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6179) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateAdded", "DateRead" },
                values: new object[] { new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6183), new DateTime(2024, 5, 7, 8, 16, 27, 890, DateTimeKind.Local).AddTicks(6182) });
        }
    }
}
