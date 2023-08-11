using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Domain.Entities;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task<GenericResponse<CommentResponseDto>> CreateComment(CommentRequestDto commentRequest);
        Task<GenericResponse<IEnumerable<Comment>>> GetCommentsForStory(Guid storyId);
    }
}