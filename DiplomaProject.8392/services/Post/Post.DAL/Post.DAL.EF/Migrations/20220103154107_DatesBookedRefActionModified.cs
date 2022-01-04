using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.DAL.EF.Migrations
{
    public partial class DatesBookedRefActionModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatesBooked_Accommodations_AccommodationId",
                table: "DatesBooked");

            migrationBuilder.AddForeignKey(
                name: "FK_DatesBooked_Accommodations_AccommodationId",
                table: "DatesBooked",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatesBooked_Accommodations_AccommodationId",
                table: "DatesBooked");

            migrationBuilder.AddForeignKey(
                name: "FK_DatesBooked_Accommodations_AccommodationId",
                table: "DatesBooked",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
