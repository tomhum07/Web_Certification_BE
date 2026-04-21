using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Certification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Certificates");
        }
    }
}
