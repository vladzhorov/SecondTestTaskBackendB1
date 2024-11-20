using Microsoft.EntityFrameworkCore;
using SecondTestTaskB1.Models;
using File = SecondTestTaskB1.Models.File;

namespace SecondTestTaskB1.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<File> Files { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasMany(f => f.Accounts)
                .WithOne(a => a.File)
                .HasForeignKey(a => a.FileId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<File>()
                .Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.FileName)
                .IsUnique();

            modelBuilder.Entity<File>()
                .Property(f => f.UploadDate)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
