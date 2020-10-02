using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodBlogApi.Migrations
{
    public partial class AltTextToPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltText",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                table: "Photos");
        }
    }
}
