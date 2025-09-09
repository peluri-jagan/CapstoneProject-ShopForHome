using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopForHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 999);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role", "Username" },
                values: new object[] { 999, "admin@gmail.com", "$2a$11$7prcM6g4x3ZcDg2eq6Kqre1FG8TzR0lxFhvS3iwA04gzNLeybM63O", "Admin", "Admin User" });
        }
    }
}
