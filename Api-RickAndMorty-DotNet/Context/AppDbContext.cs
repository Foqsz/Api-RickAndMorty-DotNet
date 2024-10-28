using Api_RickAndMorty_DotNet.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Api_RickAndMorty_DotNet.Model.CharacterModel;

namespace Api_RickAndMorty_DotNet.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CharacterModel> CharacterModels { get; set; }
        public DbSet<LocationModel> LocationModels { get; set; }
        public DbSet<EpisodesModel> EpisodesModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationModel>().HasNoKey();
            modelBuilder.Entity<CharacterModel>().HasNoKey();
            modelBuilder.Entity<EpisodesModel>().HasKey(e => e.Id);
            modelBuilder.Entity<Location>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
         
    }
}
