using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaBoomFileService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "files");

            migrationBuilder.CreateTable(
                name: "media_assets",
                schema: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    asset_type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    source_file_name = table.Column<string>(type: "text", nullable: false),
                    source_file_extension = table.Column<string>(type: "text", nullable: false),
                    source_content_type = table.Column<string>(type: "text", nullable: false),
                    source_media_type = table.Column<string>(type: "text", nullable: false),
                    source_size_bytes = table.Column<long>(type: "bigint", nullable: false),
                    expected_chunks_count = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    source_object_key_bucket = table.Column<string>(type: "text", nullable: true),
                    source_object_key_prefix = table.Column<string>(type: "text", nullable: true),
                    source_object_key_key = table.Column<string>(type: "text", nullable: true),
                    source_object_key_value = table.Column<string>(type: "text", nullable: true),
                    source_object_key_full_path = table.Column<string>(type: "text", nullable: true),
                    failure_reason = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ready_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audio_assets",
                schema: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    duration_ms = table.Column<long>(type: "bigint", nullable: true),
                    source_container = table.Column<string>(type: "text", nullable: true),
                    source_codec = table.Column<string>(type: "text", nullable: true),
                    hls_manifest_object_key_bucket = table.Column<string>(type: "text", nullable: true),
                    hls_manifest_object_key_prefix = table.Column<string>(type: "text", nullable: true),
                    hls_manifest_object_key_key = table.Column<string>(type: "text", nullable: true),
                    hls_manifest_object_key_value = table.Column<string>(type: "text", nullable: true),
                    hls_manifest_object_key_full_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audio_assets", x => x.id);
                    table.ForeignKey(
                        name: "FK_audio_assets_media_assets_id",
                        column: x => x.id,
                        principalSchema: "files",
                        principalTable: "media_assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "audio_processes",
                schema: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    audio_asset_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    progress_percentage = table.Column<int>(type: "integer", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    is_critical_error = table.Column<bool>(type: "boolean", nullable: false),
                    retry_count = table.Column<int>(type: "integer", nullable: false),
                    max_retries = table.Column<int>(type: "integer", nullable: false),
                    next_retry_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audio_processes", x => x.id);
                    table.ForeignKey(
                        name: "FK_audio_processes_audio_assets_audio_asset_id",
                        column: x => x.audio_asset_id,
                        principalSchema: "files",
                        principalTable: "audio_assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "processing_steps",
                schema: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    step_type = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    result_data = table.Column<string>(type: "jsonb", nullable: true),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    audio_process_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processing_steps", x => x.id);
                    table.ForeignKey(
                        name: "FK_processing_steps_audio_processes_audio_process_id",
                        column: x => x.audio_process_id,
                        principalSchema: "files",
                        principalTable: "audio_processes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audio_processes_audio_asset_id",
                schema: "files",
                table: "audio_processes",
                column: "audio_asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_media_assets_status_created_at",
                schema: "files",
                table: "media_assets",
                columns: new[] { "status", "created_at" });

            migrationBuilder.CreateIndex(
                name: "ix_processing_steps_audio_process_id",
                schema: "files",
                table: "processing_steps",
                column: "audio_process_id");

            migrationBuilder.CreateIndex(
                name: "ix_processing_steps_status",
                schema: "files",
                table: "processing_steps",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_processing_steps_step_type",
                schema: "files",
                table: "processing_steps",
                column: "step_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "processing_steps",
                schema: "files");

            migrationBuilder.DropTable(
                name: "audio_processes",
                schema: "files");

            migrationBuilder.DropTable(
                name: "audio_assets",
                schema: "files");

            migrationBuilder.DropTable(
                name: "media_assets",
                schema: "files");
        }
    }
}
