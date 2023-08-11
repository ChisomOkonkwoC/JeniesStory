using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<GenericResponse<CommentResponseDto>> ApproveCommentByAdmin(ApproveByAdminDto approveRequest);
        Task<GenericResponse<StoryResponseDto>> ApproveStory(ApproveStoryRequestDto approveStoryRequest);
        Task<GenericResponse<AdminResponseDto>> CreateAdmin(AdminRequestDto adminRequest, string roleId);
        Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllApprovedStoriesAsync();
    }
}