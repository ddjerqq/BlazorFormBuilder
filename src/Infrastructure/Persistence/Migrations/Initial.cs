using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "base_component_choice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    label = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    required = table.Column<bool>(type: "INTEGER", nullable: false),
                    form_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 21, nullable: false),
                    button_text = table.Column<string>(type: "TEXT", nullable: true),
                    @checked = table.Column<bool>(name: "checked", type: "INTEGER", nullable: true),
                    selected_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    input_type = table.Column<string>(type: "TEXT", nullable: true),
                    selected_numeric_value = table.Column<float>(type: "REAL", nullable: true),
                    min = table.Column<float>(type: "REAL", nullable: true),
                    max = table.Column<float>(type: "REAL", nullable: true),
                    step = table.Column<float>(type: "REAL", nullable: true),
                    selected_option = table.Column<string>(type: "TEXT", nullable: true),
                    choices = table.Column<string>(type: "TEXT", nullable: true),
                    text_value = table.Column<string>(type: "TEXT", nullable: true),
                    placeholder = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_base_component_choice", x => x.id);
                    table.ForeignKey(
                        name: "f_k_base_component_choice_form_form_id",
                        column: x => x.form_id,
                        principalTable: "form",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_base_component_choice_form_id",
                table: "base_component_choice",
                column: "form_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_component_choice");

            migrationBuilder.DropTable(
                name: "form");
        }
    }
}
