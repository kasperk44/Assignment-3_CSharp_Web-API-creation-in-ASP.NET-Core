using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Data
{
    public class SeedHelper
    {
        public static IEnumerable<Movie> GetMovieSeeds()
        {
            IEnumerable<Movie> seedMovies = new List<Movie>()
            {
                new Movie
                {
                    MovieId = 1,
                    MovieTitle = "Batman",
                    Genre = "Action",
                    ReleaseYear = 2022,
                    Director = "Matt Reeves",
                    PictureURL = @"https://m.media-amazon.com/images/M/MV5BYTExZTdhY2ItNGQ1YS00NjJlLWIxMjYtZTI1MzNlMzY0OTk4XkEyXkFqcGde",
                    TrailerURL = @"https://www.youtube.com/watch?v=mqqft2x_Aa4",
                    FranchiseId = 1
                },
                new Movie
                {
                    MovieId = 2,
                    MovieTitle = "Spiderman",
                    Genre = "Action",
                    ReleaseYear = 2022,
                    Director = "Jon Watts",
                    PictureURL = @"https://www.luxorvenray.nl/uploads/Products/product_907/SpiderManNoWayHome_132835211289696784_big.jpg",
                    TrailerURL = @"https://www.youtube.com/watch?v=JfVOs4VSpmA",
                    FranchiseId = 2
                },
                new Movie
                {
                    MovieId = 3,
                    MovieTitle = "Superman",
                    Genre = "Action",
                    ReleaseYear = 2022,
                    Director = "Zack Snyder",
                    PictureURL = "https://m.media-amazon.com/images/I/51OrrZRXTvL._AC_.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=T6DJcgm3wNY",
                    FranchiseId = 1
                }
            };

            return seedMovies;
        }
        public static IEnumerable<Character> GetCharacter()
        {
            IEnumerable<Character> seedCharacters = new List<Character>()
            {
                new Character
                {
                    CharacterId = 1,
                    FullName = "Bruce Wayne",
                    Alias = "Batman",
                    Gender = "Male",
                    PictureURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSxoj82YZjdwDq_XKN6mcpokrHJzmxFSAUQVA&usqp=CAU"
                },
                new Character
                {
                    CharacterId = 2,
                    FullName = "Peter Parker",
                    Alias = "Spiderman",
                    Gender = "Male",
                    PictureURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0-SrO0HUDdewPLFtQjygeTk1RczLQXr1lnw&usqp=CAU"
                },
                new Character
                {
                    CharacterId = 3,
                    FullName = "Clark Kent",
                    Alias = "Superman",
                    Gender = "Male",
                    PictureURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSs3QOXYB0b--1-moUgZ9r9V1F998VgGwnJJw&usqp=CAU"
                }
            };

            return seedCharacters;
        }
        public static IEnumerable<Franchise> GetFranchise()
        {
            IEnumerable<Franchise> seedFranchises = new List<Franchise>()
            {
                new Franchise
                {
                    FranchiseId = 1,
                    Name = "Warners Bros",
                    Description = "Warner Brothers Entertainment, Inc. provides video based entertainment services. The Company produces feature films, television programs, home video and DVDs, animation, interactive entertainment, and games, as well as publishes comic books. Warner Brothers Entertainment serves customers worldwide."
                },
                new Franchise
                {
                    FranchiseId = 2,
                    Name = "MCU",
                    Description = "The Marvel Cinematic Universe (MCU) is an American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios."
                }
            };

            return seedFranchises;
        }
    }
}
