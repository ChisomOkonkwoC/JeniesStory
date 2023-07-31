using JeniesStory.Domain.Entities;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateRefreshToken();
        Task<string> GenerateTokenAsync(User user, string refreshToken, string roleId);
    }
}