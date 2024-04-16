using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LAB_TH_API.Migrations
{
    /// <inheritdoc />
    public partial class DataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    CoverUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksAuthor",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAuthor", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BooksAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksAuthor_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Nguyen Van A" },
                    { 2, "Tran Van B" },
                    { 3, "Nguyễn Văn C" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Publisher A" },
                    { 2, "Publisher B" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverUrl", "DateAdded", "DateRead", "Description", "Genre", "IsRead", "PublishersId", "Rate", "Title" },
                values: new object[,]
                {
                    { 1, "url1", new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7247), new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7233), "Description for Book 1", 1, true, 1, 4, "Book 1" },
                    { 2, "url2", new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7250), new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7249), "Description for Book 2", 2, false, 2, 3, "Book 2" },
                    { 3, "url3", new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7252), new DateTime(2024, 4, 16, 10, 10, 36, 995, DateTimeKind.Local).AddTicks(7251), "Description for Book 3", 2, true, 2, 4, "Book 3" }
                });

            migrationBuilder.InsertData(
                table: "BooksAuthor",
                columns: new[] { "AuthorId", "BookId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishersId",
                table: "Books",
                column: "PublishersId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAuthor_AuthorId",
                table: "BooksAuthor",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksAuthor");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
