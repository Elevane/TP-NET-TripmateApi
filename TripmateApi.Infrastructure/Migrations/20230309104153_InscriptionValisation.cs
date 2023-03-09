using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripmateApi.Infrastructure.Migrations
{
    public partial class InscriptionValisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StepId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "validate",
                table: "inscriptions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StepId",
                table: "Users",
                column: "StepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_step_StepId",
                table: "Users",
                column: "StepId",
                principalTable: "step",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_step_StepId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StepId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StepId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "validate",
                table: "inscriptions");
        }
    }
}
