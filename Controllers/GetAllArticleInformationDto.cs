using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Logger;
using dotnet_articles_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_articles_api.Controllers
{
    [ApiController]
    [Route("api/articles/getallinformation")]
    public class GetAllArticleInformationDtoControll : ControllerBase
    {
        private readonly IArticles _articleService;
        private readonly IAppLogger _logger; // ✅ Custom logger

        public GetAllArticleInformationDtoControll(IArticles articleService, IAppLogger logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        // GET /api/articles/getallinformation
        [HttpGet]
        public ActionResult<IEnumerable<ArticleInformationDto>> GetAll()
        {
            var getAllArticleInformationDto = _articleService.GetAllArticles();
            if (getAllArticleInformationDto == null || !getAllArticleInformationDto.Any())
            {
                _logger.LogWarning($"ArticleInformationDto was not found");
                return NotFound("No articles found!!!!!!!");
            }

            return Ok(getAllArticleInformationDto);
        }
    }
}