using System.Text.Json.Serialization;

namespace dotnet_articles_api.Models
{
    public class ArticleInformation
    {
        public Guid Id { get; set; }

        // Foreign Key
        public Guid ArticleId { get; set; }

        public string? Author { get; set; }
        public string? Category { get; set; }
        public DateTime PublishedDate { get; set; }
        public int ReadTimeMinutes { get; set; }

        // Navigation Property back to Article
        [JsonIgnore]
        public Article? Article { get; set; }
    }
}