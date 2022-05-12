using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class hesham3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userOrders_AspNetUsers_ApplicationUserId1",
                table: "userOrders");

            migrationBuilder.DropIndex(
                name: "IX_userOrders_ApplicationUserId1",
                table: "userOrders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "userOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "userOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_userOrders_ApplicationUserId",
                table: "userOrders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userOrders_AspNetUsers_ApplicationUserId",
                table: "userOrders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userOrders_AspNetUsers_ApplicationUserId",
                table: "userOrders");

            migrationBuilder.DropIndex(
                name: "IX_userOrders_ApplicationUserId",
                table: "userOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "userOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "userOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userOrders_ApplicationUserId1",
                table: "userOrders",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_userOrders_AspNetUsers_ApplicationUserId1",
                table: "userOrders",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
