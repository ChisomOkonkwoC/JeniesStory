using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IStoryService
    {
        Task<GenericResponse<StoryResponseDto>> CreateStoryAsync(StoryRequestDto storyRequest);
        Task<GenericResponse<StoryResponseDto>> PublishStory(ApproveStoryRequestDto approveStoryRequest);
        Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllStoriesAsync();
        Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllPublishedStoriesAsync();
    }
}