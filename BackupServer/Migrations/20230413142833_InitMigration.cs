using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackupServer.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSaveByChange = table.Column<bool>(type: "bit", nullable: false),
                    Period = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaxNumCopy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCopies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<int>(type: "int", nullable: false),
                    ParentItemId = table.Column<int>(type: "int", nullable: false),
                    PrevCopyId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCopies_Items_ParentItemId",
                        column: x => x.ParentItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsCopies_ItemsCopies_PrevCopyId",
                        column: x => x.PrevCopyId,
                        principalTable: "ItemsCopies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                ;

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCopies_ParentItemId",
                table: "ItemsCopies",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCopies_PrevCopyId",
                table: "ItemsCopies",
                column: "PrevCopyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsCopies");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
