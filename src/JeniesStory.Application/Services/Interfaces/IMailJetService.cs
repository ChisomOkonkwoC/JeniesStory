using JeniesStory.Application.Dtos.Requests;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IMailJetService
    {
        string GetEmailTemplate(string templateName);
        Task<bool> SendEmailAsync(MailRequest mailRequest);
    }
}