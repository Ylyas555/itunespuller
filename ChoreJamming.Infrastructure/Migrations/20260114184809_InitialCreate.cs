using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreJamming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChoreHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChoreName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    SongTitle = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", maxLength: 5, nullable: false, defaultValue: 0),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoreHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoreHistories");
        }
    }
}
