using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JeniesStory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
           _commentService = commentService;
        }

        [HttpPost("[action]", Name = "CreateComment")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequestDto commentRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _commentService.CreateComment(commentRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]", Name = "GetCommentsForStory")]
        public async Task<IActionResult> GetCommentsForStory(Guid storyId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _commentService.GetCommentsForStory(storyId);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
