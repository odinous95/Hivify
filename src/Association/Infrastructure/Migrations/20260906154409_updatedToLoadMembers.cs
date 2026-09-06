using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Association.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedToLoadMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Associations_AssociationEntityId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_AssociationEntityId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AssociationEntityId",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssociationEntityId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_AssociationEntityId",
                table: "Members",
                column: "AssociationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Associations_AssociationEntityId",
                table: "Members",
                column: "AssociationEntityId",
                principalTable: "Associations",
                principalColumn: "Id");
        }
    }
}
