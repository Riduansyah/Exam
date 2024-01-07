using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<FileBlob> FileBlobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(k => k.UserId);
            });
            modelBuilder.Entity<FileBlob>(entity => {
                entity.HasKey(k => k.FileBlobId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
