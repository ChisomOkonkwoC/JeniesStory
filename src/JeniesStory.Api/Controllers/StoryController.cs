using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Services.Interfaces;
using JeniesStory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JeniesStory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        private readonly UserManager<User> _userManager;

        public StoryController(IStoryService storyService, UserManager<User> userManager)
        {
            _storyService = storyService;
            _userManager = userManager;
        }

        [HttpPost("[action]", Name = "CreateStory")]
        public async Task<IActionResult> CreateStory([FromBody]StoryRequestDto storyRequest)
        {
            try
            {
                //var roleId = HttpContext.User.Claims.Where(x => x.Type == "RoleId").Select(x => x.Value).FirstOrDefault();
                //var userId = HttpContext.User.Claims.Where(x => x.Type == "AuthorId").Select(x => x.Value).FirstOrDefault();

                          

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _storyService.CreateStoryAsync(storyRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("[action]", Name = "PublishStory")]
        public async Task<IActionResult> PublishStory([FromBody] ApproveStoryRequestDto storyRequest)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _storyService.PublishStory(storyRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]", Name = "GetAllStories")]
        public async Task<IActionResult> GetAllStories()
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _storyService.GetAllStoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]", Name = "GetAllPublishedStories")]
        public async Task<IActionResult> GetAllPublishedStories()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _storyService.GetAllPublishedStoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
