using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrMediii.Migrations
{
    public partial class CourseID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "CourseCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "CourseCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
