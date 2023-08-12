using JeniesStory.Application.Dtos.Responses;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface INewsService
    {
        Task<GenericResponse<NewsResponse>> GetNewsArticles();
    }
}