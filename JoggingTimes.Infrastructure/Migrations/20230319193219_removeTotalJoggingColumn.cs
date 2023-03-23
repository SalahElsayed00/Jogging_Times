using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoggingTimes.Infrastructure.Migrations
{
    public partial class removeTotalJoggingColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalJogging",
                table: "JoggingTimes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalJogging",
                table: "JoggingTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
