using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspIdentity.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Videos",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Videos",
                newName: "Date");
        }
    }
}
