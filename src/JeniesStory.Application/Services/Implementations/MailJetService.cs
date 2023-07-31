using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Services.Interfaces;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Services.Implementations
{
    public class MailJetService : IMailJetService
    {
        private readonly IMailjetClient _mailjet;

        public MailJetService(IMailjetClient mailjet)
        {
            _mailjet = mailjet;
        }
        public async Task<bool> SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                string mail = mailRequest.ToEmail;
                MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                .Property(Send.Messages, new JArray {
                    new JObject
                    {
                        {
                           "From",new JObject
                           {
                              {"Email","sheyeogunsanmi@gmail.com"},
                              {"Name", "JeniesStory"}
                           }
                        },
                        {
                            "To", new JArray
                            {
                               new JObject
                               {
                                  {"Email", mail },
                               }
                            }
                        },
                      {"Subject", mailRequest.Subject},
                      { "HtmlPart",  $@"{mailRequest.Body}" },
                      {"CustomId", "AppGettingStartedTest"}
                    }
                });
                MailjetResponse response = await _mailjet.PostAsync(request);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetEmailTemplate(string templateName)
        {
            var baseDir = Directory.GetCurrentDirectory();
            string folderName = "/StaticFiles/";
            var path = Path.Combine(baseDir + folderName, templateName);
            return File.ReadAllText(path);
        }
    }
}