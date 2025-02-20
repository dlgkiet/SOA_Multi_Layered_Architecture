using SOA_Layered_Arch.CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Models;

namespace SOA_Layered_Arch.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieSeries> MoviesSeries { get; set; } // Thêm DbSet MovieSeries
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; } // Thêm DbSet Ratings
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieSeriesTag> MovieSeriesTags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập khóa chính kép cho MovieSeriesTag
            modelBuilder.Entity<MovieSeriesTag>()
                .HasKey(mst => new { mst.MovieSeriesId, mst.TagId });

            // Thiết lập quan hệ với MovieSeries
            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mst => mst.MovieSeries)
                .WithMany(ms => ms.MovieSeriesTags)
                .HasForeignKey(mst => mst.MovieSeriesId);

            // Thiết lập quan hệ với Tags
            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mst => mst.Tag)
                .WithMany(t => t.MovieSeriesTags)
                .HasForeignKey(mst => mst.TagId);
        }
    }
}
