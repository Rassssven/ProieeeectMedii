using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrMediii.Migrations
{
    public partial class Price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Module",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TrainerID",
                table: "Module",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrnCourse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Module_TrainerID",
                table: "Module",
                column: "TrainerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Trainer_TrainerID",
                table: "Module",
                column: "TrainerID",
                principalTable: "Trainer",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Trainer_TrainerID",
                table: "Module");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropIndex(
                name: "IX_Module_TrainerID",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "TrainerID",
                table: "Module");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Module",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");
        }
    }
}
