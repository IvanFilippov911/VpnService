using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrakarVpn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedIp",
                table: "UserVpnDevices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedIp",
                table: "UserVpnDevices");
        }
    }
}
