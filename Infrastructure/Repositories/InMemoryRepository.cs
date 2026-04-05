using System;
using System.Collections.Generic;
using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Models;

namespace dotnet_articles_api.Infrastructure.Repositories
{
    public class InMemoryRepository : IRepository
    {
        private readonly Dictionary<Guid, Article> _articles = new();
        private readonly Dictionary<Guid, ArticleInformation> _articleInformations = new(); // ✅ New

        // ── Article Methods ──────────────────────────────

        public Article? Get(Guid id)
        {
            _articles.TryGetValue(id, out var article);
            return article;
        }

        public Guid Create(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));
            if (string.IsNullOrEmpty(article.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            article.Id = Guid.NewGuid();
            _articles[article.Id] = article;
            return article.Id;
        }

        public bool Delete(Guid id)
        {
            return _articles.Remove(id);
        }

        public bool Update(Article articleToUpdate)
        {
            if (articleToUpdate == null)
                throw new ArgumentNullException(nameof(articleToUpdate));
            if (string.IsNullOrEmpty(articleToUpdate.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            if (!_articles.ContainsKey(articleToUpdate.Id))
                return false;

            _articles[articleToUpdate.Id] = articleToUpdate;
            return true;
        }

        // ── ArticleInformation Methods ───────────────────

        public ArticleInformation? GetInformation(Guid articleId)
        {
            _articleInformations.TryGetValue(articleId, out var info);

            if (info == null)
                return null;

            // ✅ Manually attach the related Article
            _articles.TryGetValue(info.ArticleId, out var article);
            info.Article = article;

            return info;
        }

        public bool AddInformation(ArticleInformation info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            // Check the article exists first
            if (!_articles.ContainsKey(info.ArticleId))
                return false;

            info.Id = Guid.NewGuid();
            _articleInformations[info.ArticleId] = info; // ✅ Keyed by ArticleId
            return true;
        }

        public bool UpdateInformation(ArticleInformation info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            // Check the information exists first
            if (!_articleInformations.ContainsKey(info.ArticleId))
                return false;

            _articleInformations[info.ArticleId] = info; // ✅ Overwrite existing
            return true;
        }
    }
}