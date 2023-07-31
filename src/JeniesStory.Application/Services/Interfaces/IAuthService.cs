using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GenericResponse<UserResponseDto>> EmailConfirmationAsync(ConfirmEmailRequestDto confirmEmailRequest);
        Task<GenericResponse<UserResponseDto>> LoginAynsc(LoginRequestDto loginRequest);
        Task<GenericResponse<string>> RegisterAsync(RegistrationRequestDto registrationRequest);
        Task<GenericResponse<string>> ResendConfirmMailAsync(ResendConfirmationEmailDto request);
    }
}