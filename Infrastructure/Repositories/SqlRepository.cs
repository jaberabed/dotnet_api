using dotnet_articles_api.Infrastructure.Data;
using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_articles_api.Infrastructure.Repositories
{
    public class SqlRepository : IRepository, IArticles
    {
        private readonly ArticlesDbContext _context;

        public SqlRepository(ArticlesDbContext context)
        {
            _context = context;
        }

        // ── Article Methods ──────────────────────────────

        public Article? Get(Guid id)
        {
            return _context.Articles
                .Include(a => a.ArticleInformation) // ✅ Loads related info
                .FirstOrDefault(a => a.Id == id);
        }

        public Guid Create(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (string.IsNullOrEmpty(article.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            article.Id = Guid.NewGuid();

            if (article.ArticleInformation != null)
            {
                article.ArticleInformation.Id = Guid.NewGuid();
                article.ArticleInformation.ArticleId = article.Id;
            }
            _context.Articles.Add(article);
            _context.SaveChanges();
            return article.Id;
        }

        public bool Delete(Guid id)
        {
            var article = _context.Articles.Find(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Article articleToUpdate)
        {
            if (articleToUpdate == null)
                throw new ArgumentNullException(nameof(articleToUpdate));
            if (string.IsNullOrEmpty(articleToUpdate.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            var exists = _context.Articles.Find(articleToUpdate.Id);
            if (exists == null) return false;

            _context.Entry(exists).CurrentValues.SetValues(articleToUpdate);
            _context.SaveChanges();
            return true;
        }

        // ── ArticleInformation Methods ───────────────────

        public ArticleInformation? GetInformation(Guid articleId)
        {
            var article = _context.ArticleInformations
                .Include(ai => ai.Article)
                .FirstOrDefault(ai => ai.ArticleId == articleId);
            return article;
        }

        public bool AddInformation(ArticleInformation info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            var articleExists = _context.Articles.Find(info.ArticleId);
            if (articleExists == null) return false;

            info.Id = Guid.NewGuid();
            _context.ArticleInformations.Add(info);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateInformation(ArticleInformation info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            var exists = _context.ArticleInformations
                .FirstOrDefault(ai => ai.ArticleId == info.ArticleId);
            if (exists == null) return false;

            _context.Entry(exists).CurrentValues.SetValues(info);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<ArticleInformationDto> GetAllArticles()
        {
            return
              _context.ArticleInformations
                 .Include(ai => ai.Article)                  // JOIN Articles table
                 .Select(ai => new ArticleInformationDto
                 {
                     Id = ai.Id,
                     ArticleId = ai.ArticleId,
                     Author = ai.Author,
                     Category = ai.Category,
                     PublishedDate = ai.PublishedDate,
                     ReadTimeMinutes = ai.ReadTimeMinutes,
                     Title = ai.Article != null ? ai.Article.Title : string.Empty,
                     Body = ai.Article != null ? ai.Article.Body : string.Empty
                 })
                 .ToList();
        }
    }
}