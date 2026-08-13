using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBoomService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCompositionFileIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ux_compositions_file_id",
                table: "compositions",
                column: "file_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_compositions_file_id",
                table: "compositions");
        }
    }
}
