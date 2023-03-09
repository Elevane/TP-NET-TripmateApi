using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripmateApi.Infrastructure.Migrations
{
    public partial class InscriptionValisationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "StepUser",
                columns: table => new
                {
                    PassangersId = table.Column<int>(type: "int", nullable: false),
                    VoyagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepUser", x => new { x.PassangersId, x.VoyagesId });
                    table.ForeignKey(
                        name: "FK_StepUser_step_VoyagesId",
                        column: x => x.VoyagesId,
                        principalTable: "step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepUser_Users_PassangersId",
                        column: x => x.PassangersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StepUser_VoyagesId",
                table: "StepUser",
                column: "VoyagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StepUser");

            migrationBuilder.AddColumn<int>(
                name: "StepId",
                table: "Users",
                type: "int",
                nullable: true);

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
    }
}
