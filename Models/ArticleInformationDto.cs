namespace dotnet_articles_api.Models
{
    public class ArticleInformationDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int ReadTimeMinutes { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}