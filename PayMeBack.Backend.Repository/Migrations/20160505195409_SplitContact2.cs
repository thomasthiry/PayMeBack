using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace PayMeBack.Backend.Repository.Migrations
{
    public partial class SplitContact2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Contact_Split_SplitId", table: "Contact");
            migrationBuilder.CreateTable(
                name: "SplitContact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactId = table.Column<int>(nullable: true),
                    ContacttId = table.Column<int>(nullable: false),
                    SplitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplitContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SplitContact_Split_SplitId",
                        column: x => x.SplitId,
                        principalTable: "Split",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Split_SplitId",
                table: "Contact",
                column: "SplitId",
                principalTable: "Split",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Contact_Split_SplitId", table: "Contact");
            migrationBuilder.DropTable("SplitContact");
            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Split_SplitId",
                table: "Contact",
                column: "SplitId",
                principalTable: "Split",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
