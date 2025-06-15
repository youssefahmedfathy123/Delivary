using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tawsel.Migrations
{
    /// <inheritdoc />
    public partial class wkedw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivary_AspNetUsers_DelivaryManId",
                table: "Delivary");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivary_Buys_BuyId",
                table: "Delivary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivary",
                table: "Delivary");

            migrationBuilder.RenameTable(
                name: "Delivary",
                newName: "Delivaries");

            migrationBuilder.RenameIndex(
                name: "IX_Delivary_DelivaryManId",
                table: "Delivaries",
                newName: "IX_Delivaries_DelivaryManId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivary_BuyId",
                table: "Delivaries",
                newName: "IX_Delivaries_BuyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivaries",
                table: "Delivaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_AspNetUsers_DelivaryManId",
                table: "Delivaries",
                column: "DelivaryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivaries_Buys_BuyId",
                table: "Delivaries",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_AspNetUsers_DelivaryManId",
                table: "Delivaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Delivaries_Buys_BuyId",
                table: "Delivaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivaries",
                table: "Delivaries");

            migrationBuilder.RenameTable(
                name: "Delivaries",
                newName: "Delivary");

            migrationBuilder.RenameIndex(
                name: "IX_Delivaries_DelivaryManId",
                table: "Delivary",
                newName: "IX_Delivary_DelivaryManId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivaries_BuyId",
                table: "Delivary",
                newName: "IX_Delivary_BuyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivary",
                table: "Delivary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivary_AspNetUsers_DelivaryManId",
                table: "Delivary",
                column: "DelivaryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Delivary_Buys_BuyId",
                table: "Delivary",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

