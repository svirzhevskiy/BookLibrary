using BookLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.MigrateAsync();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookLib;Username=postgres;Password=postgres;");
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}