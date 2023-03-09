using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripmateApi.Infrastructure.Migrations
{
    public partial class updateInscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_step_inscriptions_InscriptionId",
                table: "step");

            migrationBuilder.DropIndex(
                name: "IX_step_InscriptionId",
                table: "step");

            migrationBuilder.DropColumn(
                name: "InscriptionId",
                table: "step");

            migrationBuilder.CreateTable(
                name: "InscriptionStep",
                columns: table => new
                {
                    InscriptionsId = table.Column<int>(type: "int", nullable: false),
                    StepsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscriptionStep", x => new { x.InscriptionsId, x.StepsId });
                    table.ForeignKey(
                        name: "FK_InscriptionStep_inscriptions_InscriptionsId",
                        column: x => x.InscriptionsId,
                        principalTable: "inscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscriptionStep_step_StepsId",
                        column: x => x.StepsId,
                        principalTable: "step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InscriptionStep_StepsId",
                table: "InscriptionStep",
                column: "StepsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InscriptionStep");

            migrationBuilder.AddColumn<int>(
                name: "InscriptionId",
                table: "step",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_step_InscriptionId",
                table: "step",
                column: "InscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_step_inscriptions_InscriptionId",
                table: "step",
                column: "InscriptionId",
                principalTable: "inscriptions",
                principalColumn: "Id");
        }
    }
}
