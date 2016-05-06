using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace PayMeBack.Backend.Repository.Migrations
{
    public partial class ContactRemoveSplitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Contact_Split_SplitId", table: "Contact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Contact_ContactId", table: "SplitContact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Split_SplitId", table: "SplitContact");
            migrationBuilder.DropColumn(name: "SplitId", table: "Contact");
            migrationBuilder.AddForeignKey(
                name: "FK_SplitContact_Contact_ContactId",
                table: "SplitContact",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_SplitContact_Split_SplitId",
                table: "SplitContact",
                column: "SplitId",
                principalTable: "Split",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Contact_ContactId", table: "SplitContact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Split_SplitId", table: "SplitContact");
            migrationBuilder.AddColumn<int>(
                name: "SplitId",
                table: "Contact",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Split_SplitId",
                table: "Contact",
                column: "SplitId",
                principalTable: "Split",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SplitContact_Contact_ContactId",
                table: "SplitContact",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_SplitContact_Split_SplitId",
                table: "SplitContact",
                column: "SplitId",
                principalTable: "Split",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
