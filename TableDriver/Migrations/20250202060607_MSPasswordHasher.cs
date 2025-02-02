using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableDriver.Migrations
{
    /// <inheritdoc />
    public partial class MSPasswordHasher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Passhash",
                table: "UserMemory",
                type: "varchar(85)",
                maxLength: 85,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Passhash",
                table: "User",
                type: "varchar(85)",
                maxLength: 85,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Passhash",
                table: "UserMemory",
                type: "varbinary(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(85)",
                oldMaxLength: 85)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Passhash",
                table: "User",
                type: "varbinary(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(85)",
                oldMaxLength: 85)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
