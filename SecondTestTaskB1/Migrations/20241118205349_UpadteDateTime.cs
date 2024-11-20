using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondTestTaskB1.Migrations
{
    /// <inheritdoc />
    public partial class UpadteDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MyDateTimeUtc",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyDateTimeUtc",
                table: "Files");
        }
    }
}
