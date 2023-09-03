using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspIdentity.Migrations
{
    /// <inheritdoc />
    public partial class idkWhatUpdateIsThis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Videos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
