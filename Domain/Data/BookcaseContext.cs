using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class BookcaseContext: DbContext
    {
        public BookcaseContext(DbContextOptions<BookcaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SavedBook> SavedBooks { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<SavedBook>().ToTable("SavedBook");
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}
