using JeniesStory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JeniesStory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
           _bookmarkService = bookmarkService;
        }

        [HttpPost("[action]", Name = "BookmarkAStory")]
        public async Task<IActionResult> BookmarkAStory(Guid storyId, string userId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookmarkService.BookmarkStory(storyId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]", Name = "GetBookmarkedStories")]
        public async Task<IActionResult> GetBookmarkedStories(string userId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookmarkService.GetBookmarkedStories(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
