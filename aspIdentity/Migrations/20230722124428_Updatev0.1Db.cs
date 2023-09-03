using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspIdentity.Migrations
{
    /// <inheritdoc />
    public partial class Updatev01Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Youtubes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Youtubes");

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
