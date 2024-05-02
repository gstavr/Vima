using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vimachem.Migrations
{
    /// <inheritdoc />
    public partial class InitDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuditLog",
                columns: new[] { "AuditLogId", "ActionType", "Changes", "EntityId", "EntityType", "ErrorMessage", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { 1, "Create", "Initial creation of Fiction category", "1", "Category", null, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3563), null },
                    { 2, "Create", "Initial creation of Non-Fiction category", "2", "Category", null, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3589), null }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1716), "This category encompasses a broad range of books that are primarily created from the imagination of the author. Fiction books may include novels, short stories, and novellas. Genres within fiction can vary widely, encompassing everything from historical fiction and fantasy to mystery, science fiction, and contemporary literature.", "Fiction", null },
                    { 2, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1795), "Non-fiction books are based on real facts and truthful accounts of events and ideas. This category includes biographies, memoirs, essays, and self-help books. Non-fiction also covers educational subjects such as history, psychology, science, and business.", "Non-Fiction", null },
                    { 3, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1797), "Books in this category are known for suspenseful plots that involve investigations and the solving of crimes. They often revolve around a mysterious event or a crime that needs to be solved, typically leading to a climactic reveal or twist.", "Mystery & Thriller", null },
                    { 4, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1798), "This category includes books that explore imaginative and futuristic concepts, alternative worlds, and advanced technology. Science fiction often deals with themes like space exploration and time travel, while fantasy books may include elements such as magic, mythical creatures, and medieval settings.", "Science Fiction & Fantasy", null },
                    { 5, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1800), "Romance books explore the theme of love and relationships between people. These stories often focus on romantic relationships from the courtship to the culmination of a love story, providing emotional narratives that may also delve into the characters’ personal growth.", "Romance", null },
                    { 6, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(1802), "Designed to appeal to children from infancy through elementary school, these books range from picture books to easy readers and early chapter books. They often teach lessons through fun stories and vibrant illustrations, helping children understand their world and sparking their imagination.", "Children’s Books", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Guest" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedDate", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, null, "Alice", "Johnson" },
                    { 2, null, "Bob", "Smith" },
                    { 3, null, "Charlie", "Brown" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3034), "Book 1 Description", "Book One", null },
                    { 2, 2, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3045), "Book 2 Description", "Book Two", null },
                    { 3, 3, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3047), "Book 3 Description", "Book Three", null },
                    { 4, 4, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3049), "Book 4 Description", "Book Four", null },
                    { 5, 5, new DateTime(2024, 5, 2, 10, 50, 24, 711, DateTimeKind.Local).AddTicks(3051), "Book 5 Description", "Book Five", null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, null, null },
                    { 2, 1, null, null },
                    { 2, 2, null, null },
                    { 2, 3, null, null },
                    { 3, 3, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
