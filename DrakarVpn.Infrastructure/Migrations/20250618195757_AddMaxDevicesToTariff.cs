using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrakarVpn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxDevicesToTariff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limitations",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Peers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserVpnDevices",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "MaxDevices",
                table: "Tariffs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Peers",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDevices",
                table: "Tariffs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserVpnDevices",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Limitations",
                table: "Tariffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Peers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "Peers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
