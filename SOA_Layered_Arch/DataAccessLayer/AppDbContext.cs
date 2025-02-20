using SOA_Layered_Arch.CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace SOA_Layered_Arch.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieSeriesTag> MovieSeriesTags { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) :
       base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieSeriesTag>()
                .HasKey(mst => new { mst.MovieId, mst.TagId }); // Đổi MovieSeriesId → MovieId

            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mst => mst.Movie)
                .WithMany(m => m.MovieSeriesTags)
                .HasForeignKey(mst => mst.MovieId); // Đổi MovieSeriesId → MovieId

            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mst => mst.Tag)
                .WithMany(t => t.MovieSeriesTags)
                .HasForeignKey(mst => mst.TagId);
        }
    }
}
