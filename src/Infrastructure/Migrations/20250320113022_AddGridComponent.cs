using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGridComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "data",
                table: "base_component_choice",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dimensions",
                table: "base_component_choice",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "base_component_choice");

            migrationBuilder.DropColumn(
                name: "dimensions",
                table: "base_component_choice");
        }
    }
}
