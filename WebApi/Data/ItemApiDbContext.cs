
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ItemApiDbContext : DbContext
    {   private  DbSet<BakeryItems> _bakeryItems;
        private  DbSet<BooksItems> _booksItems;
        private  DbSet<ElectronicsItems> _electronicsItems;
        public ItemApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BakeryItems> BakeryItems { get => _bakeryItems; set => _bakeryItems = value; }
        public DbSet<BooksItems> BooksItems { get => _booksItems; set => _booksItems = value; }
        public DbSet<ElectronicsItems> ElectronicsItems { get => _electronicsItems; set => _electronicsItems = value; }
    }
}
