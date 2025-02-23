using Microsoft.EntityFrameworkCore;
using TestLayer.Core.Entities;

namespace TestLayer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PageEntity> Pages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageLikeEntity>()
                .HasOne(pl => pl.Page)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PageId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<PageLikeEntity>()
                .HasOne(pl => pl.User)
                .WithMany()
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }




    }
}
