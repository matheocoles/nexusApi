using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_EventRecurrence_EventRecurrenceId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ActivityId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EventRecurrenceId",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateTimeStart",
                table: "Sessions",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateTimeEnd",
                table: "Sessions",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtraActivityId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClassId",
                table: "Sessions",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ExtraActivityId",
                table: "Sessions",
                column: "ExtraActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SportId",
                table: "Sessions",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_ClassId",
                table: "Sessions",
                column: "ClassId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_ExtraActivityId",
                table: "Sessions",
                column: "ExtraActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_SportId",
                table: "Sessions",
                column: "SportId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_EventRecurrence_EventRecurrenceId",
                table: "Sessions",
                column: "EventRecurrenceId",
                principalTable: "EventRecurrence",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_ClassId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_ExtraActivityId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Activities_SportId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_EventRecurrence_EventRecurrenceId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ClassId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ExtraActivityId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SportId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ExtraActivityId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventRecurrenceId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateTimeStart",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateTimeEnd",
                table: "Sessions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ActivityId",
                table: "Sessions",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Activities_ActivityId",
                table: "Sessions",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_EventRecurrence_EventRecurrenceId",
                table: "Sessions",
                column: "EventRecurrenceId",
                principalTable: "EventRecurrence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
