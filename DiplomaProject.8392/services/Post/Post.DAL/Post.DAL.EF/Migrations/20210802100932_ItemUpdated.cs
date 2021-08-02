using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.DAL.EF.Migrations
{
    public partial class ItemUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "Specificities",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "Rules",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "Facilities",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Specificities");

            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "Facilities");
        }
    }
}
