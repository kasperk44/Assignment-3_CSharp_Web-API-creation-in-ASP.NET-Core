using Assignment3.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Character> Character { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Franchise> Franchise { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(SeedHelper.GetMovieSeeds());
            modelBuilder.Entity<Character>().HasData(SeedHelper.GetCharacter());
            modelBuilder.Entity<Franchise>().HasData(SeedHelper.GetFranchise());
            modelBuilder.Entity<Character>()
            .HasMany(p => p.Movie)
            .WithMany(m => m.Character)
            .UsingEntity<Dictionary<string, object>>(
                "CharacterMovie",
                r => r.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                l => l.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                je =>
                {
                    je.HasKey("CharacterId", "MovieId");
                    je.HasData(
                        new { CharacterId = 1, MovieId = 1 },
                        new { CharacterId = 2, MovieId = 2 },
                        new { CharacterId = 3, MovieId = 3 },
                        new { CharacterId = 3, MovieId = 1 }
                    );
                });

        }
    }
}
