using System;
using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Logger;
using dotnet_articles_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_articles_api.Controllers
{
    [ApiController]
    [Route("api/articles/{articleId}/information")]
    public class ArticleInformationController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IAppLogger _logger; // ✅ Custom logger

        public ArticleInformationController(IRepository repository, IAppLogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET /api/articles/{articleId}/information
        [HttpGet]
        public IActionResult Get(Guid articleId)
        {
            _logger.LogInfo($"Getting information for article with id: {articleId}");

            var info = _repository.GetInformation(articleId);

            if (info == null)
            {
                _logger.LogWarning($"Information for article with id: {articleId} was not found");
                return NotFound(new { message = "Article information not found" });
            }

            return Ok(info);
        }

        // POST /api/articles/{articleId}/information
        [HttpPost]
        public IActionResult Add(Guid articleId, [FromBody] ArticleInformation info)
        {
            if (info == null)
            {
                _logger.LogError("ArticleInformation object is null");
                return BadRequest();
            }

            info.ArticleId = articleId;
            var added = _repository.AddInformation(info);

            if (!added)
            {
                _logger.LogWarning($"Article with id: {articleId} was not found");
                return NotFound(new { message = "Article not found" });
            }

            _logger.LogInfo($"Information added for article with id: {articleId}");
            return Created($"/api/articles/{articleId}/information", info);
        }

        // PUT /api/articles/{articleId}/information
        [HttpPut]
        public IActionResult Update(Guid articleId, [FromBody] ArticleInformation info)
        {
            if (info == null)
            {
                _logger.LogError("ArticleInformation object is null");
                return BadRequest();
            }

            info.ArticleId = articleId;
            var updated = _repository.UpdateInformation(info);

            if (!updated)
            {
                _logger.LogWarning($"Information for article with id: {articleId} was not found");
                return NotFound(new { message = "Article information not found" });
            }

            _logger.LogInfo($"Information updated for article with id: {articleId}");
            return Ok();
        }
    }
}