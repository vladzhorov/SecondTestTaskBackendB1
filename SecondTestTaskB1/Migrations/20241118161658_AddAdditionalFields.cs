using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondTestTaskB1.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileName",
                table: "Files",
                column: "FileName",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileName",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Accounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadDate",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
