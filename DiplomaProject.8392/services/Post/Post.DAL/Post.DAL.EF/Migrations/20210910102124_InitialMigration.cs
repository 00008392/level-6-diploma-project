using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.DAL.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specificities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specificities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferencePoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomsNo = table.Column<int>(type: "int", nullable: true),
                    BathroomsNo = table.Column<int>(type: "int", nullable: true),
                    BedsNo = table.Column<int>(type: "int", nullable: true),
                    MaxGuestsNo = table.Column<int>(type: "int", nullable: false),
                    SquareMeters = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsWholeApartment = table.Column<bool>(type: "bit", nullable: true),
                    MovingInTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    MovingOutTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Accommodations_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationFacilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    OtherItem = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationFacilities_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationFacilities_Facilities_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationPhotos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationPhotos_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    OtherItem = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationRules_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationRules_Rules_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationSpecificities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    OtherItem = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationSpecificities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationSpecificities_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationSpecificities_Specificities_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Specificities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationFacilities_AccommodationId",
                table: "AccommodationFacilities",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationFacilities_ItemId_AccommodationId",
                table: "AccommodationFacilities",
                columns: new[] { "ItemId", "AccommodationId" },
                unique: true,
                filter: "OtherItem is null");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationFacilities_ItemId_AccommodationId_OtherItem",
                table: "AccommodationFacilities",
                columns: new[] { "ItemId", "AccommodationId", "OtherItem" },
                unique: true,
                filter: "[OtherItem] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationPhotos_AccommodationId",
                table: "AccommodationPhotos",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRules_AccommodationId",
                table: "AccommodationRules",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRules_ItemId_AccommodationId",
                table: "AccommodationRules",
                columns: new[] { "ItemId", "AccommodationId" },
                unique: true,
                filter: "OtherItem is null");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRules_ItemId_AccommodationId_OtherItem",
                table: "AccommodationRules",
                columns: new[] { "ItemId", "AccommodationId", "OtherItem" },
                unique: true,
                filter: "[OtherItem] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_CategoryId",
                table: "Accommodations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_OwnerId",
                table: "Accommodations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationSpecificities_AccommodationId",
                table: "AccommodationSpecificities",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationSpecificities_ItemId_AccommodationId",
                table: "AccommodationSpecificities",
                columns: new[] { "ItemId", "AccommodationId" },
                unique: true,
                filter: "OtherItem is null");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationSpecificities_ItemId_AccommodationId_OtherItem",
                table: "AccommodationSpecificities",
                columns: new[] { "ItemId", "AccommodationId", "OtherItem" },
                unique: true,
                filter: "[OtherItem] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_Email",
                table: "Owners",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationFacilities");

            migrationBuilder.DropTable(
                name: "AccommodationPhotos");

            migrationBuilder.DropTable(
                name: "AccommodationRules");

            migrationBuilder.DropTable(
                name: "AccommodationSpecificities");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Specificities");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
