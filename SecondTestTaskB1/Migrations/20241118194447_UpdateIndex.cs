using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondTestTaskB1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Files_FileId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Files_FileId",
                table: "Accounts",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Files_FileId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Files_FileId",
                table: "Accounts",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
