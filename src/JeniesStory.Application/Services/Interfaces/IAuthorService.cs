using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;

namespace JeniesStory.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<GenericResponse<CommentResponseDto>> ApproveCommentByAuthor(ApproveByAuthorDto approveRequest);
        Task<GenericResponse<AuthorResponseDto>> CreateAuthor(AuthorRequestDto authorRequest, string roleId);
        Task<GenericResponse<CommentResponseDto>> DisapproveCommentByAuthor(ApproveByAuthorDto approveRequest);
        Task<GenericResponse<AuthorResponseDto>> GetAuthorById(Guid id);
    }
}