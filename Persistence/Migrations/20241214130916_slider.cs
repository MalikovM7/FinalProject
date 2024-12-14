using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class slider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "Sliders",
                newName: "Subtitle");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Sliders",
                newName: "Desc");
        }
    }
}
