using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.DAL.EF.Migrations
{
    public partial class FeedbacksAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationFeedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationFeedbacks_Accommodations_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccommodationFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserFeedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeedbacks_Users_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFeedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationFeedbacks_ItemId",
                table: "AccommodationFeedbacks",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationFeedbacks_UserId",
                table: "AccommodationFeedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_ItemId",
                table: "UserFeedbacks",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_UserId",
                table: "UserFeedbacks",
                column: "UserId");

            migrationBuilder.Sql(@"create trigger UserDeleted
                                    on Users
                                    instead of delete
                                    as
                                    begin
                                    set nocount on
                                    delete UserFeedbacks from UserFeedbacks
                                    join deleted
                                    on UserFeedbacks.ItemId = deleted.Id;
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
            migrationBuilder.DropTable(
                name: "AccommodationFeedbacks");

            migrationBuilder.DropTable(
                name: "UserFeedbacks");
            migrationBuilder.Sql(@"drop trigger UserDeleted");
        }
    }
}
