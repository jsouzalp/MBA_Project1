using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Repositories.Migrations.BlogDb
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AUTHOR",
                columns: table => new
                {
                    AUTHOR_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    IDENTITY_USER = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    NAME = table.Column<string>(type: "Varchar", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AUTHOR", x => x.AUTHOR_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_POST",
                columns: table => new
                {
                    POST_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AUTHOR_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    DATE = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TITLE = table.Column<string>(type: "Varchar", maxLength: 100, nullable: false),
                    MESSAGE = table.Column<string>(type: "Varchar", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_POST", x => x.POST_ID);
                    table.ForeignKey(
                        name: "FK_TB_POST_002",
                        column: x => x.AUTHOR_ID,
                        principalTable: "TB_AUTHOR",
                        principalColumn: "AUTHOR_ID");
                });

            migrationBuilder.CreateTable(
                name: "TB_COMMENT",
                columns: table => new
                {
                    COMMENT_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    POST_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    COMENT_AUTHOR_ID = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    DATE = table.Column<DateTime>(type: "DateTime", nullable: false),
                    MESSAGE = table.Column<string>(type: "Varchar", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_COMMENT", x => x.COMMENT_ID);
                    table.ForeignKey(
                        name: "FK_TB_COMMENT_001",
                        column: x => x.POST_ID,
                        principalTable: "TB_POST",
                        principalColumn: "POST_ID");
                    table.ForeignKey(
                        name: "FK_TB_COMMENT_002",
                        column: x => x.COMENT_AUTHOR_ID,
                        principalTable: "TB_AUTHOR",
                        principalColumn: "AUTHOR_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IDX_TB_COMMENT_01",
                table: "TB_COMMENT",
                column: "POST_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_TB_COMMENT_02",
                table: "TB_COMMENT",
                column: "COMENT_AUTHOR_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_TB_COMMENT_03",
                table: "TB_COMMENT",
                column: "DATE");

            migrationBuilder.CreateIndex(
                name: "IDX_TB_POST_01",
                table: "TB_POST",
                column: "AUTHOR_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_TB_POST_02",
                table: "TB_POST",
                column: "DATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_COMMENT");

            migrationBuilder.DropTable(
                name: "TB_POST");

            migrationBuilder.DropTable(
                name: "TB_AUTHOR");
        }
    }
}
