using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNewsProject.Migrations
{
    /// <inheritdoc />
    public partial class SubUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "Subscription");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresDate",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "RenewedDate",
                table: "Subscription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscriptionTypeId",
                table: "Subscription",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_SubscriptionType_SubscriptionTypeId",
                table: "Subscription",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_SubscriptionType_SubscriptionTypeId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_SubscriptionTypeId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "RenewedDate",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Subscription");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Subscription",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpiresDate",
                table: "Subscription",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreateDate",
                table: "Subscription",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "Subscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
