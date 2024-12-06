using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GESTIONAR_PROMOCIONES.Migrations
{
    /// <inheritdoc />
    public partial class CategoryExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Categories",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Categories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
