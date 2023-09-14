using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VotingService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Vote");

            migrationBuilder.CreateTable(
                name: "VoteTopics",
                schema: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TopicName = table.Column<string>(type: "text", nullable: false),
                    TopicDescription = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteTopicChoices",
                schema: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChoiceName = table.Column<string>(type: "text", nullable: false),
                    VoteTopicId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteTopicChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteTopicChoices_VoteTopics_VoteTopicId",
                        column: x => x.VoteTopicId,
                        principalSchema: "Vote",
                        principalTable: "VoteTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVotes",
                schema: "Vote",
                columns: table => new
                {
                    VoteTopicChoiceId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVotes", x => new { x.UserId, x.VoteTopicChoiceId });
                    table.ForeignKey(
                        name: "FK_UserVotes_VoteTopicChoices_VoteTopicChoiceId",
                        column: x => x.VoteTopicChoiceId,
                        principalSchema: "Vote",
                        principalTable: "VoteTopicChoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_VoteTopicChoiceId",
                schema: "Vote",
                table: "UserVotes",
                column: "VoteTopicChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteTopicChoices_VoteTopicId",
                schema: "Vote",
                table: "VoteTopicChoices",
                column: "VoteTopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVotes",
                schema: "Vote");

            migrationBuilder.DropTable(
                name: "VoteTopicChoices",
                schema: "Vote");

            migrationBuilder.DropTable(
                name: "VoteTopics",
                schema: "Vote");
        }
    }
}
