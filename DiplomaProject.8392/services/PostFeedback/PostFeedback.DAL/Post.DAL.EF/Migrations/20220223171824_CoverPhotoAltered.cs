using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostFeedback.DAL.EF.Migrations
{
    public partial class CoverPhotoAltered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhoto",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "Photos",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "Photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverPhoto",
                table: "Posts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
