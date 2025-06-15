using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tawsel.Migrations
{
    /// <inheritdoc />
    public partial class fdelee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day of delivery",
                table: "Buys",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Buys",
                newName: "Date of delivery");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Buys",
                newName: "Day of delivery");

            migrationBuilder.RenameColumn(
                name: "Date of delivery",
                table: "Buys",
                newName: "Date");
        }
    }
}
