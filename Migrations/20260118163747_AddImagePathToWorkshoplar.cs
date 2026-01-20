using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouncilAtelier.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToWorkshoplar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Workshoplar",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Workshoplar");
        }
    }
}
