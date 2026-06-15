using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddCoffeeUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeUsages_CoffeeProducts_ProductID",
                table: "CoffeeUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeUsages_Users_UserID",
                table: "CoffeeUsages");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeUsages_ProductID",
                table: "CoffeeUsages");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "CoffeeUsages",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UsageDate",
                table: "CoffeeUsages",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "CoffeeUsages",
                newName: "UsedCups");

            migrationBuilder.RenameColumn(
                name: "CupsConsumed",
                table: "CoffeeUsages",
                newName: "MonthlyLimit");

            migrationBuilder.RenameColumn(
                name: "UsageID",
                table: "CoffeeUsages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CoffeeUsages_UserID",
                table: "CoffeeUsages",
                newName: "IX_CoffeeUsages_UserId");

            migrationBuilder.AddColumn<int>(
                name: "CoffeeProductProductID",
                table: "CoffeeUsages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeUsages_CoffeeProductProductID",
                table: "CoffeeUsages",
                column: "CoffeeProductProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeUsages_CoffeeProducts_CoffeeProductProductID",
                table: "CoffeeUsages",
                column: "CoffeeProductProductID",
                principalTable: "CoffeeProducts",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeUsages_Users_UserId",
                table: "CoffeeUsages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeUsages_CoffeeProducts_CoffeeProductProductID",
                table: "CoffeeUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_CoffeeUsages_Users_UserId",
                table: "CoffeeUsages");

            migrationBuilder.DropIndex(
                name: "IX_CoffeeUsages_CoffeeProductProductID",
                table: "CoffeeUsages");

            migrationBuilder.DropColumn(
                name: "CoffeeProductProductID",
                table: "CoffeeUsages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CoffeeUsages",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UsedCups",
                table: "CoffeeUsages",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "MonthlyLimit",
                table: "CoffeeUsages",
                newName: "CupsConsumed");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "CoffeeUsages",
                newName: "UsageDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CoffeeUsages",
                newName: "UsageID");

            migrationBuilder.RenameIndex(
                name: "IX_CoffeeUsages_UserId",
                table: "CoffeeUsages",
                newName: "IX_CoffeeUsages_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeUsages_ProductID",
                table: "CoffeeUsages",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeUsages_CoffeeProducts_ProductID",
                table: "CoffeeUsages",
                column: "ProductID",
                principalTable: "CoffeeProducts",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoffeeUsages_Users_UserID",
                table: "CoffeeUsages",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
