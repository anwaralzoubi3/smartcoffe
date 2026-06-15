using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class AddCoffeeShopNameToPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // إضافة عمود اسم الكوفي شوب فقط
            migrationBuilder.AddColumn<string>(
                name: "CoffeeShopName",
                table: "Plans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // حذف العمود عند الرجوع
            migrationBuilder.DropColumn(
                name: "CoffeeShopName",
                table: "Plans");
        }
    }
}