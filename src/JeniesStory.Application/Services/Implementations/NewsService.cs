using AutoMapper;
using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Application.Services.Interfaces;
using JeniesStory.Domain.Entities;
using JeniesStory.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public NewsService(IConfiguration config, IHttpClientFactory clientFactory, AppDbContext dbContext, IMapper mapper)
        {
            _config = config;
            _clientFactory = clientFactory;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GenericResponse<NewsResponse>> GetNewsArticles()
        {
            try
            {
                var httpClient = _clientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Clear();

                var apiKey = _config["NewsArticle:apiKey"];
                var baseUrl = _config["NewsArticle:apiUrl"];
                var apiUrl = $"{baseUrl}&apiKey={apiKey}";

                var response = await httpClient.GetAsync(apiUrl);
                var result = await response.Content.ReadFromJsonAsync<NewsResponse>();
                if (result.articles != null)
                {   
                    foreach(var article in result.articles)
                    {

                        var newsSource = new Domain.Entities.NewsSource();
                        newsSource.Name = article.source.name;
                        newsSource.newsId = article.source.id;

                        var newNews = new News();
                        newNews.Source = newsSource;
                        newNews.Content = article.content;
                        newNews.Title = article.title;
                        newNews.Description = article.description;
                        newNews.Author = article.author;
                        newNews.PublishedAt = article.publishedAt;
                        newNews.UrlToImage = article.urlToImage;
                       // var newNews = _mapper.Map<News>(article);
                       
                        _dbContext.NewsArticles.Add(newNews);
                    }
                        

                    
                    await _dbContext.SaveChangesAsync();
                    //var newsResponse = _mapper.Map<NewsResponse>(articleResponse);
                    return GenericResponse<NewsResponse>.Success
                   ("News articles fetched Successfully", result);
                }
                return GenericResponse<NewsResponse>.Fail("The news Articles does not exist");
            }
            catch (Exception ex)
            {

                throw new Exception($"GetNewsArticle Exception: {ex.Message}");
            }
        }
       
    }
}
