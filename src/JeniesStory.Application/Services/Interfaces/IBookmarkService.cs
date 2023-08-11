using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Domain.Entities;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<GenericResponse<string>> BookmarkStory(Guid storyId, string userId);
        Task<GenericResponse<IEnumerable<Story>>> GetBookmarkedStories(string userId);
    }
}