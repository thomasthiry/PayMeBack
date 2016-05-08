using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace PayMeBack.Backend.Repository.Migrations
{
    public partial class SplitContactAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Contact_ContactId", table: "SplitContact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Split_SplitId", table: "SplitContact");
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "SplitContact",
                nullable: true);
            migrationBuilder.AddColumn<decimal>(
                name: "Owes",
                table: "SplitContact",
                nullable: false,
                defaultValue: 0m);
            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "SplitContact",
                nullable: false,
                defaultValue: 0m);
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
            migrationBuilder.DropColumn(name: "Comments", table: "SplitContact");
            migrationBuilder.DropColumn(name: "Owes", table: "SplitContact");
            migrationBuilder.DropColumn(name: "Paid", table: "SplitContact");
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
