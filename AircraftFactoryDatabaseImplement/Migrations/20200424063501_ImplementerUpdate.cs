using Microsoft.EntityFrameworkCore.Migrations;

namespace AircraftFactoryDatabaseImplement.Migrations
{
    public partial class ImplementerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImplementerFIO",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImplementerId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Implementers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImplementerFIO = table.Column<string>(nullable: true),
                    WorkingTime = table.Column<int>(nullable: false),
                    PauseTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Implementers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Implementers");

            migrationBuilder.DropColumn(
                name: "ImplementerFIO",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ImplementerId",
                table: "Orders");
        }
    }
}
