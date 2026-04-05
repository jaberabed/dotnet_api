using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Models;
using Microsoft.AspNetCore.Mvc;
using dotnet_articles_api.Logger; // ✅ Add this

[ApiController]
[Route("api/articles")]
public class ArticlesController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IAppLogger _logger; // ✅ Use your custom logger

    public ArticlesController(IRepository repository, IAppLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        _logger.LogInfo($"Getting article with id: {id}"); // ✅
        var article = _repository.Get(id);

        if (article == null)
        {
            _logger.LogWarning($"Article with id: {id} was not found"); // ✅
            return NotFound(new { message = "Article not found" });
        }

        return Ok(article);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Article article)
    {
        if (article == null)
        {
            _logger.LogError("Article object is null"); // ✅
            return BadRequest();
        }

        var id = _repository.Create(article);
        _logger.LogInfo($"Article created with id: {id}"); // ✅
        return Created($"/api/articles/{id}", new { id });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
        {
            _logger.LogWarning($"Article with id: {id} was not found"); // ✅
            return NotFound(new { message = "Article not found" });
        }

        _logger.LogInfo($"Article with id: {id} was deleted"); // ✅
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] Article article)
    {
        if (article == null)
        {
            _logger.LogError("Article object is null"); // ✅
            return BadRequest();
        }

        article.Id = id;
        var updated = _repository.Update(article);

        if (!updated)
        {
            _logger.LogWarning($"Article with id: {id} was not found"); // ✅
            return NotFound(new { message = "Article not found" });
        }

        _logger.LogInfo($"Article with id: {id} was updated"); // ✅
        return Ok();
    }
}