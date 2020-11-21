using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerOrderRESTService.EFLayer.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderTable",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderTable",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
