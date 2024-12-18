using BookStore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext>options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Citats> Citats { get; set; }



    }
}
