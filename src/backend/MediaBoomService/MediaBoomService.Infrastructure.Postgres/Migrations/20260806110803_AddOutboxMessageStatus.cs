using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBoomService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxMessageStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "outbox_messages",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "outbox_messages");
        }
    }
}
