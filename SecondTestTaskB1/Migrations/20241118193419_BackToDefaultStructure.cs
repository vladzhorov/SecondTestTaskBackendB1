using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondTestTaskB1.Migrations
{
    /// <inheritdoc />
    public partial class BackToDefaultStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ParentId",
                table: "Accounts",
                column: "ParentId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
