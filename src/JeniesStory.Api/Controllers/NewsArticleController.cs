using JeniesStory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JeniesStory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsArticleController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsArticleController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("action", Name = "GetNewsArticles")]
        public async Task<IActionResult> GetNewsArticles()
        {
            var result = await _newsService.GetNewsArticles();
            return Ok(result);
        }
    }
}
