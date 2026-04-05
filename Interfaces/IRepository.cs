using dotnet_articles_api.Models;

namespace dotnet_articles_api.Interfaces
{
    public interface IRepository
    {
        // Returns a found article or null.
        Article? Get(Guid id);

        // Creates a new article and returns its identifier.
        // Throws an exception if article is null.
        // Throws an exception if title is null or empty.
        Guid Create(Article article);

        // Returns true if deleted, false if article not found.
        bool Delete(Guid id);

        // Returns true if updated, false if article not found.
        // Throws an exception if articleToUpdate is null.
        // Throws an exception if title is null or empty.
        bool Update(Article articleToUpdate);

        // ArticleInformation methods ✅ New
        ArticleInformation? GetInformation(Guid articleId);

        bool AddInformation(ArticleInformation info);

        bool UpdateInformation(ArticleInformation info);
    }
}