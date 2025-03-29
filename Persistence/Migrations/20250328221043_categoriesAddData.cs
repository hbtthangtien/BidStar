using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class categoriesAddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Điện tử: điện thoại, laptop, v.v.", "Electronics" },
                    { 2, "Thời trang: quần áo, phụ kiện", "Fashion" },
                    { 3, "Đồ gia dụng, nội thất, cây cảnh", "Home & Garden" },
                    { 4, "Sách, truyện tranh, giáo trình", "Books" },
                    { 5, "Vật phẩm sưu tầm, đồ cổ", "Collectibles" },
                    { 6, "Trang sức, đồng hồ", "Jewelry & Watches" },
                    { 7, "Thể thao, dã ngoại", "Sports & Outdoors" },
                    { 8, "Mỹ phẩm, chăm sóc sức khỏe", "Health & Beauty" },
                    { 9, "Đồ chơi, mô hình", "Toys & Hobbies" },
                    { 10, "Ô tô, xe máy, xe đạp", "Vehicles" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
