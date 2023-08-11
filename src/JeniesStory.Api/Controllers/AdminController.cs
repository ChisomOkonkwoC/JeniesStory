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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
           _adminService = adminService;
        }

        [HttpPost("[action]", Name = "CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequestDto adminRequest, Guid roleId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _adminService.CreateAdmin(adminRequest, roleId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]", Name = "ApproveStory")]
        public async Task<IActionResult> ApproveStory([FromBody] ApproveStoryRequestDto approveRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _adminService.ApproveStory(approveRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]", Name = "ApproveCommentsByAdmin")]
        public async Task<IActionResult> ApproveCommentsByAdmin([FromBody] ApproveByAdminDto approveRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _adminService.ApproveCommentByAdmin(approveRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]", Name = "GetAllApprovedStories")]
        public async Task<IActionResult> GetAllApprovedStories()
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _adminService.GetAllApprovedStoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
