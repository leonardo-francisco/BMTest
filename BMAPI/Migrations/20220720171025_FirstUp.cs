using Microsoft.EntityFrameworkCore.Migrations;

namespace BMAPI.Migrations
{
    public partial class FirstUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelRoutes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelRoutes");
        }
    }
}
