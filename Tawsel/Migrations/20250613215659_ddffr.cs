using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tawsel.Migrations
{
    /// <inheritdoc />
    public partial class ddffr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delivary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelivaryManId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivary_AspNetUsers_DelivaryManId",
                        column: x => x.DelivaryManId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Delivary_Buys_BuyId",
                        column: x => x.BuyId,
                        principalTable: "Buys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivary_BuyId",
                table: "Delivary",
                column: "BuyId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivary_DelivaryManId",
                table: "Delivary",
                column: "DelivaryManId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivary");
        }
    }
}
