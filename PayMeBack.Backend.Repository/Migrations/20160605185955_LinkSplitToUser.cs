using Microsoft.Data.Entity.Migrations;

namespace PayMeBack.Backend.Repository.Migrations
{
    public partial class LinkSplitToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Contact_ContactId", table: "SplitContact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Split_SplitId", table: "SplitContact");
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Split",
                nullable: false,
                defaultValue: 2);
            migrationBuilder.AddForeignKey(
                name: "FK_Split_AppUser_UserId",
                table: "Split",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
            migrationBuilder.DropForeignKey(name: "FK_Split_AppUser_UserId", table: "Split");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Contact_ContactId", table: "SplitContact");
            migrationBuilder.DropForeignKey(name: "FK_SplitContact_Split_SplitId", table: "SplitContact");
            migrationBuilder.DropColumn(name: "UserId", table: "Split");
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
