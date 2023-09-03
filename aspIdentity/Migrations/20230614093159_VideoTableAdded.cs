using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspIdentity.Migrations
{
    /// <inheritdoc />
    public partial class VideoTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_Id",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Video",
                table: "Video");

            migrationBuilder.RenameTable(
                name: "Video",
                newName: "Videos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_Id",
                table: "Videos",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_Id",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "Video");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Video",
                table: "Video",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_Id",
                table: "Video",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
