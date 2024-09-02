using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotionNotifications.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationRoot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "text", nullable: false),
                    NotionIdProperty = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    AlreadyNotified = table.Column<bool>(type: "int", nullable: false),
                    EventDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Occurence = table.Column<int>(type: "int", nullable: false),
                    Categories = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRoot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEventRoot",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    EventDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    OldObjectAsJsonString = table.Column<string>(type: "text", nullable: false),
                    NewObjectAsJsonString = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEventRoot", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationRoot");

            migrationBuilder.DropTable(
                name: "TransactionEventRoot");
        }
    }
}
