using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Twajd_Back_End.DataAccess.Migrations
{
    public partial class secund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHours_Manager_ManagerId",
                table: "WorkHours");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "WorkHours",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("670e0b21-8f65-42a1-8bd1-f171b5580408"),
                column: "ConcurrencyStamp",
                value: "a84991a4-fd96-47ed-ac01-070cc85ec0c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f5dc82f-22c7-4eb1-bba9-5c442f611f8c"),
                column: "ConcurrencyStamp",
                value: "4ca2e9b7-0442-407f-8772-ebff5ff17fda");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b59feb1b-4c4f-4b0e-99d8-349f2310b850"),
                column: "ConcurrencyStamp",
                value: "4b90f9bf-4682-4666-afc6-1baea0362ab6");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHours_Manager_ManagerId",
                table: "WorkHours",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHours_Manager_ManagerId",
                table: "WorkHours");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                table: "WorkHours",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("670e0b21-8f65-42a1-8bd1-f171b5580408"),
                column: "ConcurrencyStamp",
                value: "1e17fc81-69e6-40af-b5ca-efa21660ce33");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f5dc82f-22c7-4eb1-bba9-5c442f611f8c"),
                column: "ConcurrencyStamp",
                value: "5415086e-5e0d-495f-ae86-580c35c845c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b59feb1b-4c4f-4b0e-99d8-349f2310b850"),
                column: "ConcurrencyStamp",
                value: "4f28496f-40f0-4579-89e5-82cdbcbc28d0");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHours_Manager_ManagerId",
                table: "WorkHours",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
