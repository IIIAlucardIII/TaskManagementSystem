using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Persistence.Migrations
{
    public partial class Updatetask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "EditedAt",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditedAt",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
