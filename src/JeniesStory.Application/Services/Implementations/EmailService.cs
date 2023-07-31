using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Services.Interfaces;
using JeniesStory.Application.Utilities;
using JeniesStory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IMailJetService _mailJet;

        public EmailService(IMailJetService mailJet)
        {
            _mailJet = mailJet;
        }
        public async Task<bool> SendAConfirmationEmailAsync(User user, string token, string templatefile, string redirectUrl)
        {
            var template = _mailJet.GetEmailTemplate(templatefile);
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;
            var userName = textInfo.ToTitleCase(user.FirstName);
            var encodedToken = TokenConverter.EncodeToken(token);
            var lenngth = encodedToken.Length;
            var link = (templatefile == "ForgotPassword.html") ? $"{redirectUrl}?email={user.Email}&token={encodedToken}"
                : $"{redirectUrl}?email={user.Email}&token={encodedToken}";

            template = template.Replace("{User}", $"{userName}");
            template = template.Replace("{link}", link);

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email,
                Body = template,
                Subject = (templatefile == "ForgotPassword.html") ? "Reset Password" : "Registration"
            };
            return await _mailJet.SendEmailAsync(mailRequest);
        }
    }
}