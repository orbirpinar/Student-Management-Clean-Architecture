using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MakeAccountToValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Teachers",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Teachers",
                newName: "Firstname");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Teachers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Teachers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Teachers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Teachers",
                newName: "FirstName");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Accounts_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
