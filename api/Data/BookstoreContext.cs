using Microsoft.EntityFrameworkCore;
using Fisher.Bookstore.Models;
using Fisher.Bookstore.Api;

namespace Fisher.Bookstore.Api.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set;}
        
    }
}