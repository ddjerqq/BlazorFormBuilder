using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Relation_to_UserForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "form",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "i_x_form_user_id",
                table: "form",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_form_user_user_id",
                table: "form",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey( 
                name: "f_k_form_user_user_id",
                table: "form");

            migrationBuilder.DropIndex(
                name: "i_x_form_user_id",
                table: "form");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "form");
        }
    }
}
