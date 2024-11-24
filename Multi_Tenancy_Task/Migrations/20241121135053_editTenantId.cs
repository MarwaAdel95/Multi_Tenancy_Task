using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multi_Tenancy_Task.Migrations
{
    /// <inheritdoc />
    public partial class editTenantId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tenancy",
                newName: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Tenancy",
                newName: "Id");
        }
    }
}
