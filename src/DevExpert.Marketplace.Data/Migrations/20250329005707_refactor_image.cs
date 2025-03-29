using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevExpert.Marketplace.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactor_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Images",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
