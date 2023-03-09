using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripmateApi.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Position_PositionArrivalId",
                table: "Step");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Position_PositionDepartId",
                table: "Step");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Trajets_TrajetId",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_PositionArrivalId",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Step_PositionDepartId",
                table: "Step");

            migrationBuilder.RenameTable(
                name: "Step",
                newName: "step");

            migrationBuilder.RenameIndex(
                name: "IX_Step_TrajetId",
                table: "step",
                newName: "IX_step_TrajetId");

            migrationBuilder.AlterColumn<int>(
                name: "TrajetId",
                table: "step",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_step",
                table: "step",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_step_Id",
                table: "step",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_step_PositionArrivalId",
                table: "step",
                column: "PositionArrivalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_step_PositionDepartId",
                table: "step",
                column: "PositionDepartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_step_Position_PositionArrivalId",
                table: "step",
                column: "PositionArrivalId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_step_Position_PositionDepartId",
                table: "step",
                column: "PositionDepartId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_step_Trajets_TrajetId",
                table: "step",
                column: "TrajetId",
                principalTable: "Trajets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_step_Position_PositionArrivalId",
                table: "step");

            migrationBuilder.DropForeignKey(
                name: "FK_step_Position_PositionDepartId",
                table: "step");

            migrationBuilder.DropForeignKey(
                name: "FK_step_Trajets_TrajetId",
                table: "step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_step",
                table: "step");

            migrationBuilder.DropIndex(
                name: "IX_step_Id",
                table: "step");

            migrationBuilder.DropIndex(
                name: "IX_step_PositionArrivalId",
                table: "step");

            migrationBuilder.DropIndex(
                name: "IX_step_PositionDepartId",
                table: "step");

            migrationBuilder.RenameTable(
                name: "step",
                newName: "Step");

            migrationBuilder.RenameIndex(
                name: "IX_step_TrajetId",
                table: "Step",
                newName: "IX_Step_TrajetId");

            migrationBuilder.AlterColumn<int>(
                name: "TrajetId",
                table: "Step",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                table: "Step",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Step_PositionArrivalId",
                table: "Step",
                column: "PositionArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_PositionDepartId",
                table: "Step",
                column: "PositionDepartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Position_PositionArrivalId",
                table: "Step",
                column: "PositionArrivalId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Position_PositionDepartId",
                table: "Step",
                column: "PositionDepartId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Trajets_TrajetId",
                table: "Step",
                column: "TrajetId",
                principalTable: "Trajets",
                principalColumn: "Id");
        }
    }
}
