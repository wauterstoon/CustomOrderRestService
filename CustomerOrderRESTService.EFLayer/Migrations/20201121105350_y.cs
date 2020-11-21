using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerOrderRESTService.EFLayer.Migrations
{
    public partial class y : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderTable",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_Customer_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
