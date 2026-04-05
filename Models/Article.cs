namespace dotnet_articles_api.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }

        // Navigation Property to ArticleInformation

        public ArticleInformation? ArticleInformation { get; set; }
    }
}