using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.DAL.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomsNo = table.Column<int>(type: "int", nullable: true),
                    BathroomsNo = table.Column<int>(type: "int", nullable: true),
                    BedsNo = table.Column<int>(type: "int", nullable: true),
                    MaxGuestsNo = table.Column<int>(type: "int", nullable: false),
                    SquareMeters = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWholeApartment = table.Column<bool>(type: "bit", nullable: true),
                    MovingInTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    MovingOutTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);  
                });

            migrationBuilder.CreateTable(
                name: "BookingRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<long>(type: "bigint", nullable: true),
                    AccommodationId = table.Column<long>(type: "bigint", nullable: true),
                    GuestNo = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingRequests_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingRequestUser",
                columns: table => new
                {
                    BookingRequestsAsCoTravelerId = table.Column<long>(type: "bigint", nullable: false),
                    CoTravelersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRequestUser", x => new { x.BookingRequestsAsCoTravelerId, x.CoTravelersId });
                    table.ForeignKey(
                        name: "FK_BookingRequestUser_BookingRequests_BookingRequestsAsCoTravelerId",
                        column: x => x.BookingRequestsAsCoTravelerId,
                        principalTable: "BookingRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRequestUser_Users_CoTravelersId",
                        column: x => x.CoTravelersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_OwnerId",
                table: "Accommodations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_AccommodationId",
                table: "BookingRequests",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequests_GuestId",
                table: "BookingRequests",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRequestUser_CoTravelersId",
                table: "BookingRequestUser",
                column: "CoTravelersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.Sql(@"create trigger AccommodationDeleted
                                    on Accommodations
                                    instead of delete
                                    as
                                    begin
                                    set nocount on
                                    delete BookingRequests from BookingRequests
                                    join deleted
                                    on BookingRequests.AccommodationId = deleted.Id;
                                    delete Accommodations from Accommodations
                                    join deleted
                                    on Accommodations.Id = deleted.Id;
                                    end");
            migrationBuilder.Sql(@"create trigger UserDeleted
                                    on Users
                                    instead of delete
                                    as
                                    begin
                                    set nocount on
                                    delete BookingRequests from BookingRequests
                                    join deleted
                                    on BookingRequests.GuestId = deleted.Id;
                                    delete Accommodations from Accommodations
                                    join deleted
                                    on Accommodations.OwnerId = deleted.Id;
                                    delete Users from Users
                                    join deleted 
                                    on Users.Id = deleted.Id;
                                    end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger AccommodationDeleted");
            migrationBuilder.Sql(@"drop trigger UserDeleted"); 

            migrationBuilder.DropTable(
                name: "BookingRequestUser");

            migrationBuilder.DropTable(
                name: "BookingRequests");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Users");

        }
    }
}
