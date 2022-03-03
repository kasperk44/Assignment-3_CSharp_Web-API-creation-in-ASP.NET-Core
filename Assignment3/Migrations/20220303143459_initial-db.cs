using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment3.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PictureURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.FranchiseId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PictureURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TrailerURL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movie_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "FranchiseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharacterId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "CharacterId", "Alias", "FullName", "Gender", "PictureURL" },
                values: new object[,]
                {
                    { 1, "Batman", "Bruce Wayne", "Male", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxoj82YZjdwDq_XKN6mcpokrHJzmxFSAUQVA&usqp=CAU" },
                    { 2, "Spiderman", "Peter Parker", "Male", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0-SrO0HUDdewPLFtQjygeTk1RczLQXr1lnw&usqp=CAU" },
                    { 3, "Superman", "Clark Kent", "Male", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSs3QOXYB0b--1-moUgZ9r9V1F998VgGwnJJw&usqp=CAU" }
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "FranchiseId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Warner Brothers Entertainment, Inc. provides video based entertainment services. The Company produces feature films, television programs, home video and DVDs, animation, interactive entertainment, and games, as well as publishes comic books. Warner Brothers Entertainment serves customers worldwide.", "Warners Bros" },
                    { 2, "The Marvel Cinematic Universe (MCU) is an American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios.", "MCU" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "Director", "FranchiseId", "Genre", "MovieTitle", "PictureURL", "ReleaseYear", "TrailerURL" },
                values: new object[] { 1, "Matt Reeves", 1, "Action", "Batman", "https://m.media-amazon.com/images/M/MV5BYTExZTdhY2ItNGQ1YS00NjJlLWIxMjYtZTI1MzNlMzY0OTk4XkEyXkFqcGde", 2022, "https://www.youtube.com/watch?v=mqqft2x_Aa4" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "Director", "FranchiseId", "Genre", "MovieTitle", "PictureURL", "ReleaseYear", "TrailerURL" },
                values: new object[] { 3, "Zack Snyder", 1, "Action", "Superman", "https://m.media-amazon.com/images/I/51OrrZRXTvL._AC_.jpg", 2022, "https://www.youtube.com/watch?v=T6DJcgm3wNY" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "MovieId", "Director", "FranchiseId", "Genre", "MovieTitle", "PictureURL", "ReleaseYear", "TrailerURL" },
                values: new object[] { 2, "Jon Watts", 2, "Action", "Spiderman", "https://www.luxorvenray.nl/uploads/Products/product_907/SpiderManNoWayHome_132835211289696784_big.jpg", 2022, "https://www.youtube.com/watch?v=JfVOs4VSpmA" });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 3, 3 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MovieId",
                table: "CharacterMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FranchiseId",
                table: "Movie",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Franchise");
        }
    }
}
