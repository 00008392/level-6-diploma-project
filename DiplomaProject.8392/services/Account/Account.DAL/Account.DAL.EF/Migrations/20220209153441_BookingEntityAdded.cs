using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Account.DAL.EF.Migrations
{
    public partial class BookingEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GuestId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuestId",
                table: "Bookings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OwnerId",
                table: "Bookings",
                column: "OwnerId");
            //trigger fired when user is deleted
            //bookings are deleted first, then user
            migrationBuilder.Sql(@"create trigger UserDeleted
                                   on Users
                                   instead of delete
                                   as
                                   begin
                                   set nocount on
                                   delete Bookings from Bookings
                                   join deleted
                                   on Bookings.GuestId = deleted.Id;
                                   delete Bookings from Bookings
                                   join deleted
                                   on Bookings.OwnerId = deleted.Id;
                                   delete Users from Users
                                   join deleted 
                                   on Users.Id = deleted.Id;
                                   end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
            migrationBuilder.Sql(@"drop trigger UserDeleted");
        }
    }
}
