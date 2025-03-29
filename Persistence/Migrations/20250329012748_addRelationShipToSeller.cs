using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRelationShipToSeller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "AuctionSessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionSessions_SellerId",
                table: "AuctionSessions",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionSessions_Sellers_SellerId",
                table: "AuctionSessions",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionSessions_Sellers_SellerId",
                table: "AuctionSessions");

            migrationBuilder.DropIndex(
                name: "IX_AuctionSessions_SellerId",
                table: "AuctionSessions");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "AuctionSessions");
        }
    }
}
