using Domain.Models;
using Microsoft.EntityFrameworkCore;

//using MySql.Data.EntityFrameworkCore.Extensions;

//NOTE Change DB Shema -> 1) Create migratio > dotnet ef migrations add [migrationName] 2) Examine Up and Down methods 3) dotnet ef database update


namespace Domain.Data
{
    public class HranaContext : DbContext
    {

        public HranaContext(DbContextOptions<HranaContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<SavedBook> SavedBooks { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            //modelBuilder.Entity<Book>(entity =>
            //{
            //    entity.HasKey(e => e.BookId);
            //    entity.Property(e => e.Title).IsRequired();
            //    entity.HasOne(d => d.Publisher)
            //      .WithMany(p => p.Books);
            //});

            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<SavedBook>().ToTable("SavedBook");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
