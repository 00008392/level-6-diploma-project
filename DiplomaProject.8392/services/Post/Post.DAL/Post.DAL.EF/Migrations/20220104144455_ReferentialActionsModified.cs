using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.DAL.EF.Migrations
{
    public partial class ReferentialActionsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationFeedbacks_Accommodations_ItemId",
                table: "AccommodationFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Users_OwnerId",
                table: "Accommodations");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationFeedbacks_Accommodations_ItemId",
                table: "AccommodationFeedbacks",
                column: "ItemId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Users_OwnerId",
                table: "Accommodations",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationFeedbacks_Accommodations_ItemId",
                table: "AccommodationFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Users_OwnerId",
                table: "Accommodations");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationFeedbacks_Accommodations_ItemId",
                table: "AccommodationFeedbacks",
                column: "ItemId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Users_OwnerId",
                table: "Accommodations",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
