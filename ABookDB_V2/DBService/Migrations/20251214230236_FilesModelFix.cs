using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBService.Migrations
{
    /// <inheritdoc />
    public partial class FilesModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Books_BookModelId",
                table: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_Files_BookModelId",
                table: "Files",
                newName: "IX_Files_bookId");

            migrationBuilder.RenameColumn(
                name: "BookModelId",
                table: "Files",
                newName: "bookId");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Reviews",
                newName: "text");

            /*migrationBuilder.AddColumn<int>(
                name: "bookId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

            /*migrationBuilder.CreateIndex(
                name: "IX_Files_bookId",
                table: "Files",
                column: "bookId");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Books_bookId",
                table: "Files",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Books_bookId",
                table: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_Files_bookId",
                table: "Files",
                newName: "IX_Files_BookModelId");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "Files",
                newName: "BookModelId");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Reviews",
                newName: "Text");

            /*migrationBuilder.AddColumn<int>(
                name: "BookModelId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_BookModelId",
                table: "Files",
                column: "BookModelId");*/

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Books_BookModelId",
                table: "Files",
                column: "BookModelId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
