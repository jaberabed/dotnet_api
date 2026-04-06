using dotnet_articles_api.Models;

namespace dotnet_articles_api.Interfaces
{
    public interface IArticles
    {
        //GetAllArticleInformationDto? GetAllArticles();
        IEnumerable<ArticleInformationDto> GetAllArticles();
    }
}