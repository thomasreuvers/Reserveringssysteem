using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeMoenenAPI.Migrations
{
    public partial class AddExtraReservationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingEndDateTime",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingEndDateTime",
                table: "Reservations");
        }
    }
}
