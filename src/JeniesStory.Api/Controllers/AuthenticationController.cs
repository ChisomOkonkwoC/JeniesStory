using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JeniesStory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                return Ok(await _authService.LoginAynsc(loginRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "Register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestDto userRequest)
        {
            try
            {
                if (!TryValidateModel(userRequest))
                {
                    return BadRequest();
                }

                var user = await _authService.RegisterAsync((userRequest));
                return Ok(user);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDto confirmEmailRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var result = await _authService.EmailConfirmationAsync(confirmEmailRequestDTO);
                if (!result.Status)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "ResendEmailConfirmation")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendConfirmationEmailDto request)
        {
            try
            {
                return Ok(await _authService.ResendConfirmMailAsync(request));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
