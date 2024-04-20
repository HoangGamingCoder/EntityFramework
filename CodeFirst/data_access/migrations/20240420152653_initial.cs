using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.data_access.migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.KhoaId);
                });

            migrationBuilder.CreateTable(
                name: "Lops",
                columns: table => new
                {
                    LopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lops", x => x.LopId);
                    table.ForeignKey(
                        name: "FK_Lops_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    SinhVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuoi = table.Column<int>(type: "int", nullable: false),
                    LopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.SinhVienId);
                    table.ForeignKey(
                        name: "FK_SinhViens_Lops_LopId",
                        column: x => x.LopId,
                        principalTable: "Lops",
                        principalColumn: "LopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lops_KhoaId",
                table: "Lops",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_LopId",
                table: "SinhViens",
                column: "LopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "Lops");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
