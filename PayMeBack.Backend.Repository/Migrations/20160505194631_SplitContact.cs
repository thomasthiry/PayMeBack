using Microsoft.Data.Entity.Migrations;

namespace PayMeBack.Backend.Repository.Migrations
{
    public partial class SplitContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Contact_Split_SplitId", table: "Contact");
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
