﻿using JeniesStory.Application.Services.Implementations;
using JeniesStory.Application.Services.Interfaces;
using Mailjet.Client;

namespace JeniesStory.Api.Configuration
{
    public static class DiServiceConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthenticationService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMailJetService, MailJetService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            {
                client.UseBasicAuthentication(config.GetSection("MailJetKeys")["ApiKey"], config.GetSection("MailJetKeys")["ApiSecret"]);
            });
        }
    }
}
