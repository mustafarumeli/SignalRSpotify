using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRSpotify.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserConnectionIds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserConnectionIds",
                nullable: true);
        }
    }
}
