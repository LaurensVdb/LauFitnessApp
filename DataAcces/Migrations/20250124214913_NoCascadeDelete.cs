using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NoCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_Exercises_ExerciseId",
                table: "WorkoutSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
