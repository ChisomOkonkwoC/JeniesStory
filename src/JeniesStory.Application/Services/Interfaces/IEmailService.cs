using JeniesStory.Domain.Entities;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendAConfirmationEmailAsync(User user, string token, string templatefile, string redirectUrl);
    }
}