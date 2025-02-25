using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class Bodypart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyPart",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "BodyPartId",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bodyparts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodyparts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_BodyPartId",
                table: "Exercises",
                column: "BodyPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Bodyparts_BodyPartId",
                table: "Exercises",
                column: "BodyPartId",
                principalTable: "Bodyparts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Bodyparts_BodyPartId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Bodyparts");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_BodyPartId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "BodyPartId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "BodyPart",
                table: "Exercises",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
