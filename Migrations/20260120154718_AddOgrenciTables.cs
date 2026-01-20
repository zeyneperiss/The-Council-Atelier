using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouncilAtelier.Migrations
{
    /// <inheritdoc />
    public partial class AddOgrenciTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciProgramKayitlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciProgramKayitlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciProgramKayitlari_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciProgramKayitlari_Programlar_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciWorkshopKayitlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciWorkshopKayitlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciWorkshopKayitlari_Ogrenciler_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciWorkshopKayitlari_Workshoplar_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshoplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProgramKayitlari_OgrenciId",
                table: "OgrenciProgramKayitlari",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciProgramKayitlari_ProgramId",
                table: "OgrenciProgramKayitlari",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciWorkshopKayitlari_OgrenciId",
                table: "OgrenciWorkshopKayitlari",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciWorkshopKayitlari_WorkshopId",
                table: "OgrenciWorkshopKayitlari",
                column: "WorkshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciProgramKayitlari");

            migrationBuilder.DropTable(
                name: "OgrenciWorkshopKayitlari");

            migrationBuilder.DropTable(
                name: "Ogrenciler");
        }
    }
}
