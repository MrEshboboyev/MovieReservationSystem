using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReservationSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addPriceToScheduleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Schedules",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Schedules");
        }
    }
}
