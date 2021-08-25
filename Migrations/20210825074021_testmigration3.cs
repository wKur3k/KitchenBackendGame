using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBackendGame.Migrations
{
    public partial class testmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Heroes_HeroId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HeroId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HeroId",
                table: "Users",
                column: "HeroId",
                unique: true,
                filter: "[HeroId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Heroes_HeroId",
                table: "Users",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Heroes_HeroId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HeroId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HeroId",
                table: "Users",
                column: "HeroId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Heroes_HeroId",
                table: "Users",
                column: "HeroId",
                principalTable: "Heroes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
