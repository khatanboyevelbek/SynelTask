using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SynelTask.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailHome",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Id_EmailHome",
                table: "Employees",
                columns: new[] { "Id", "EmailHome" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Id_EmailHome",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "EmailHome",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
