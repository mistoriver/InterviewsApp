using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewsApp.Data.Migrations
{
    public partial class CompanyRatePerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CompanyRate",
                table: "Positions",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyRate",
                table: "Positions");
        }
    }
}
