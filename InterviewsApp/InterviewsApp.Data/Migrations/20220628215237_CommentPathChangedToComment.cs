using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewsApp.Data.Migrations
{
    public partial class CommentPathChangedToComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathToComment",
                table: "Positions",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "PathToComment",
                table: "Interviews",
                newName: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Positions",
                newName: "PathToComment");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Interviews",
                newName: "PathToComment");
        }
    }
}
