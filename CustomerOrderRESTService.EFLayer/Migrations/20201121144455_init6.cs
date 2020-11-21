using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerOrderRESTService.EFLayer.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "OrderTable");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "CustomerTable");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "OrderTable",
                newName: "IX_OrderTable_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTable",
                table: "OrderTable",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerTable",
                table: "CustomerTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "CustomerTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerId",
                table: "OrderTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTable",
                table: "OrderTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerTable",
                table: "CustomerTable");

            migrationBuilder.RenameTable(
                name: "OrderTable",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "CustomerTable",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTable_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
