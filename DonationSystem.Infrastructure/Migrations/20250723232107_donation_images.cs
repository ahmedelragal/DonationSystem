using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class donation_images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DonationImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationImage_Donations_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationImage_DonationId",
                table: "DonationImage",
                column: "DonationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationImage");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Donations");
        }
    }
}
