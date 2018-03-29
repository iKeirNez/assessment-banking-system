using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bank.Migrations
{
    public partial class UpdateAccountFieldsToBeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // get rid of all null values before altering column
            migrationBuilder.UpdateData("Accounts", "AccountNumber", null, "Pin", "0000");
            migrationBuilder.UpdateData("Accounts", "Pin", null, "Pin", "0000");
            migrationBuilder.UpdateData("Accounts", "FirstName", null, "FirstName", "John");
            migrationBuilder.UpdateData("Accounts", "LastName", null, "LastName", "Smith");
            migrationBuilder.UpdateData("Accounts", "Address", null, "Address", "");

            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pin",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
