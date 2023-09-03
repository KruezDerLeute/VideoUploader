using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspIdentity.Migrations
{
    /// <inheritdoc />
    public partial class YoutubeLinkCommentAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Youtubes_YoutubeLinkId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_YoutubeLinkId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "YoutubeLinkId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "VideoComments");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VideoId",
                table: "VideoComments",
                newName: "IX_VideoComments_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "VideoComments",
                newName: "IX_VideoComments_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "VideoComments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoComments",
                table: "VideoComments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "YoutubeLinkComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoutubeLinkEntityId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeLinkComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeLinkComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YoutubeLinkComments_Youtubes_YoutubeLinkEntityId",
                        column: x => x.YoutubeLinkEntityId,
                        principalTable: "Youtubes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeLinkComments_UserId",
                table: "YoutubeLinkComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeLinkComments_YoutubeLinkEntityId",
                table: "YoutubeLinkComments",
                column: "YoutubeLinkEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_AspNetUsers_UserId",
                table: "VideoComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_Videos_VideoId",
                table: "VideoComments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_AspNetUsers_UserId",
                table: "VideoComments");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_Videos_VideoId",
                table: "VideoComments");

            migrationBuilder.DropTable(
                name: "YoutubeLinkComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoComments",
                table: "VideoComments");

            migrationBuilder.RenameTable(
                name: "VideoComments",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_VideoComments_VideoId",
                table: "Comments",
                newName: "IX_Comments_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_VideoComments_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "YoutubeLinkId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_YoutubeLinkId",
                table: "Comments",
                column: "YoutubeLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoId",
                table: "Comments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Youtubes_YoutubeLinkId",
                table: "Comments",
                column: "YoutubeLinkId",
                principalTable: "Youtubes",
                principalColumn: "Id");
        }
    }
}
