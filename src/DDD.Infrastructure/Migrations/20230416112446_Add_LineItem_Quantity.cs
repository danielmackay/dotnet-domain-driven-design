using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_LineItem_Quantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "LineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LineItems");
        }
    }
}
