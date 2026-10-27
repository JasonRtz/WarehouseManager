using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCaseInsensitiveSku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "citext",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_SKU_Length",
                table: "Products",
                sql: "char_length(\"SKU\") <= 30");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_SKU",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_SKU_Length",
                table: "Products");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "SKU",
                table: "Products",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext",
                oldMaxLength: 30);
        }
    }
}
