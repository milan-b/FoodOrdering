using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


//using MySql.Data.EntityFrameworkCore.Extensions;

//NOTE Change DB Shema -> 1) Create migratio > dotnet ef migrations add [migrationName] 2) Examine Up and Down methods 3) dotnet ef database update
// this works -> 1) add-migration [migrationName] -context [contextName] 2) Examine Up and Down methods   3) setup "Default project" to Domain i PM>
// 4) update-database -context [contextName]
// Delete DB -> 1) drop-database -context [contextName]
// Run docker containter 1) docker run -e MYSQL_ROOT_PASSWORD=pa$$w0rd -p 3306:33060 mysql
// this works -> 1) docker ps -a 2) docker container start d7819d679cb7
namespace Domain.Data
{
    public class HranaContext : DbContext
    {

        public HranaContext(DbContextOptions<HranaContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<SavedBook> SavedBooks { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Hrana> Hrana { get; set; }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Meni> Menii { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<Ocjena> Ocjene { get; set; }
        public DbSet<Prilog> Prilozi { get; set; }

        public DbSet<HranaPrilog> HranaPrilozi { get; set; }
        public DbSet<HranaMeni> HranaMeni { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Book>(entity =>
            //{
            //    entity.HasKey(e => e.BookId);
            //    entity.Property(e => e.Title).IsRequired();
            //    entity.HasOne(d => d.Publisher)
            //      .WithMany(p => p.Books);
            //});

            //modelBuilder.Entity<HranaPrilog>().HasKey(hp => new { hp.HranaId, hp.PrilogId });
            modelBuilder.Entity<HranaMeni>().HasKey(hm => new { hm.HranaId, hm.MeniId });
            modelBuilder.Entity<HranaPrilog>().HasKey(hp => new { hp.HranaId, hp.PrilogId });

            modelBuilder.Entity<Meni>().HasIndex(m => m.Datum).IsUnique(true);
            modelBuilder.Entity<Meni>().Property(m => m.Datum).HasColumnType("Date");

            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<SavedBook>().ToTable("SavedBook");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Hrana>().ToTable("Hrana");
            modelBuilder.Entity<Komentar>().ToTable("Komentar");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Meni>().ToTable("Meni");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Ocjena>().ToTable("Ocjena");
            modelBuilder.Entity<Prilog>().ToTable("Prilog");
            modelBuilder.Entity<HranaPrilog>().ToTable("HranaPrilog");
            modelBuilder.Entity<HranaMeni>().ToTable("HranaMeni");
        }
    }


}
