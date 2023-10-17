using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokemon.Areas.Identity.Data;

namespace Pokemon.Models
{
    public class PokemonDbContext : IdentityDbContext<UsersModel>
    {
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FavoritesModel>().HasKey(f => new { f.UserId, f.PokemonId});
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<FavoritesModel> Favorites { get; set; }
    }
}
