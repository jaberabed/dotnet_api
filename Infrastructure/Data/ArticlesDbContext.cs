using dotnet_articles_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_articles_api.Infrastructure.Data
{
    public class ArticlesDbContext : DbContext
    {
        public ArticlesDbContext(DbContextOptions<ArticlesDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleInformation> ArticleInformations { get; set; }

        public DbSet<ArticleInformationDto> ArticleInformationDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Article → One ArticleInformation
            modelBuilder.Entity<Article>()
                .HasOne(a => a.ArticleInformation)
                .WithOne(ai => ai.Article)
                .HasForeignKey<ArticleInformation>(ai => ai.ArticleId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes info when article is deleted
        }
    }
}