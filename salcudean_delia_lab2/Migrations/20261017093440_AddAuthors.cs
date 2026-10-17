

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salcudean_delia_lab2.Migrations
{
    public partial class AddAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID);
                });

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
                INSERT INTO [Author] ([FirstName], [LastName])
                VALUES ('Liviu', 'Rebreanu'), ('Ioan', 'Slavici');

                UPDATE [Book]
                SET [AuthorID] = (
                    SELECT [ID] FROM [Author]
                    WHERE [FirstName] = 'Liviu' AND [LastName] = 'Rebreanu'
                )
                WHERE LTRIM(RTRIM([Author])) = 'Liviu Rebreanu';

                UPDATE [Book]
                SET [AuthorID] = (
                    SELECT [ID] FROM [Author]
                    WHERE [FirstName] = 'Ioan' AND [LastName] = 'Slavici'
                )
                WHERE LTRIM(RTRIM([Author])) = 'Ioan Slavici';
            ");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Book",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorID",
                table: "Book",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorID",
                table: "Book",
                column: "AuthorID",
                principalTable: "Author",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorID",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorID",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE [Book]
                SET [Author] = 'Liviu Rebreanu'
                WHERE [AuthorID] = (
                    SELECT [ID] FROM [Author]
                    WHERE [FirstName] = 'Liviu' AND [LastName] = 'Rebreanu'
                );

                UPDATE [Book]
                SET [Author] = 'Ioan Slavici'
                WHERE [AuthorID] = (
                    SELECT [ID] FROM [Author]
                    WHERE [FirstName] = 'Ioan' AND [LastName] = 'Slavici'
                );
            ");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
