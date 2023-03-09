using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripmateApi.Infrastructure.Migrations
{
    public partial class addInscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InscriptionId",
                table: "step",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "inscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrajetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inscriptions_Trajets_TrajetId",
                        column: x => x.TrajetId,
                        principalTable: "Trajets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_step_InscriptionId",
                table: "step",
                column: "InscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_inscriptions_TrajetId",
                table: "inscriptions",
                column: "TrajetId");

            migrationBuilder.CreateIndex(
                name: "IX_inscriptions_UserId",
                table: "inscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_step_inscriptions_InscriptionId",
                table: "step",
                column: "InscriptionId",
                principalTable: "inscriptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_step_inscriptions_InscriptionId",
                table: "step");

            migrationBuilder.DropTable(
                name: "inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_step_InscriptionId",
                table: "step");

            migrationBuilder.DropColumn(
                name: "InscriptionId",
                table: "step");
        }
    }
}
