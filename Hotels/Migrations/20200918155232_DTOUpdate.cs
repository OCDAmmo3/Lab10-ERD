using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotels.Migrations
{
    public partial class DTOUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "HotelRooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "HotelRooms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
