using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetAndReact.Migrations
{
    public partial class moviedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Title", "Year" },
                values: new object[] { 1, "It's a Wonderful Life", 1946 });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Title", "Year" },
                values: new object[] { 2, "His Girl Friday", 1940 });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Title", "Year" },
                values: new object[] { 3, "The Thin Man", 1934 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
