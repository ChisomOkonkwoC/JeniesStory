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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("[action]", Name = "CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorRequestDto authorRequest, Guid roleId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authorService.CreateAuthor(authorRequest, roleId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]", Name = "ApproveCommentByAuthor")]
        public async Task<IActionResult> ApproveCommentByAuthor([FromBody] ApproveByAuthorDto approveByAuthor)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authorService.ApproveCommentByAuthor(approveByAuthor);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]", Name = "DisapproveCommentByAuthor")]
        public async Task<IActionResult> DisapproveCommentByAuthor([FromBody] ApproveByAuthorDto approveByAuthor)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authorService.DisapproveCommentByAuthor(approveByAuthor);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
