using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gender>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Actor>().Property(p => p.Name).HasMaxLength(150);
            modelBuilder.Entity<Actor>().Property(p => p.Picture).IsUnicode();
        }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Actor> Actors { get; set; }
    }
}
