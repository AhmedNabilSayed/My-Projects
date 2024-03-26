using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourismPlaces.Migrations
{
    public partial class addIsDeletedAndIsApprovedToPlaceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Places",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Places",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Places");
        }
    }
}
