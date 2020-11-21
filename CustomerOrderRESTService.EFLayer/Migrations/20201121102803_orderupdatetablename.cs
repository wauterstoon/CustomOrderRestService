using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerOrderRESTService.EFLayer.Migrations
{
    public partial class orderupdatetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "OrderTable");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "OrderTable",
                newName: "IX_OrderTable_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTable",
                table: "OrderTable",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTable",
                table: "OrderTable");

            migrationBuilder.RenameTable(
                name: "OrderTable",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTable_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
