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
            Database.Migrate();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BookLibrary;Username=postgres;Password=postgres;");
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}