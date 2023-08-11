using AutoMapper;
using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Application.Services.Interfaces;
using JeniesStory.Domain.Entities;
using JeniesStory.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthorService(UserManager<User> userManager, AppDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GenericResponse<AuthorResponseDto>> CreateAuthor(AuthorRequestDto authorRequest, string roleId)
        {
            try
            {
                var author = await _userManager.FindByEmailAsync(authorRequest.Email);
                if (author == null)
                {
                    return GenericResponse<AuthorResponseDto>.Fail
                          ($"User with the email {authorRequest.Email} does not exist", HttpStatusCode.BadRequest);
                }
                var roleName = "Author";
                var authorName = await _roleManager.FindByNameAsync(roleName);
                var authorId = await _roleManager.FindByIdAsync(roleId);
                if (authorId == null || roleId != authorName.Id)
                {
                    return GenericResponse<AuthorResponseDto>.Fail
                          ($"The Id {roleId} does not exist or Id mismatch", HttpStatusCode.BadRequest);
                }

                var newAuthor = _mapper.Map<Author>(authorRequest);
                newAuthor.CreatedAt = DateTime.Now;
                newAuthor.RoleId = Guid.Parse(authorId.Id);
                newAuthor.UserId = authorRequest.UserId;
                _dbContext.Add(newAuthor);
                await _dbContext.SaveChangesAsync();

                var response = _mapper.Map<AuthorResponseDto>(newAuthor);
                return GenericResponse<AuthorResponseDto>.Success
                      ("Author was Created Successfully", response);
            }
            catch (Exception ex)
            {

                throw new Exception($"Exception error message: {ex.Message}");
            }

        }

        public async Task<GenericResponse<CommentResponseDto>> ApproveCommentByAuthor(ApproveByAuthorDto approveRequest)
        {
            try
            {
                var comment = await _dbContext.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == approveRequest.CommentId);
                if (comment == null)
                {
                    return GenericResponse<CommentResponseDto>.Fail
                          ($"Story with the Id {approveRequest.CommentId} does not exist", HttpStatusCode.BadRequest);
                }
                if (comment.IsApproved == false)
                {
                    var roleName = "Author";
                    var adminName = await _roleManager.FindByNameAsync(roleName);
                    //var admin = await _roleManager.GetRoleIdAsync(adminName);
                    var adminId = adminName.Id;
                    if (Guid.TryParse(adminId, out Guid adminIdCheck))
                    {
                        if (approveRequest.RoleId != adminIdCheck)
                        {
                            return GenericResponse<CommentResponseDto>.Fail
                              ($"User {approveRequest.RoleId} is not authorized to approve a comment", HttpStatusCode.BadRequest);
                        }
                    }

                    comment.CreatedAt = DateTime.Now;
                    comment.AdminId = null;
                    comment.AuthorId = approveRequest.AuthorId;
                    comment.IsApproved = true;

                    _dbContext.Update(comment);

                    await _dbContext.SaveChangesAsync();

                    var response = _mapper.Map<CommentResponseDto>(comment);
                    return GenericResponse<CommentResponseDto>.Success
                          ("Story has been approved Successfully", response);
                }
                return new();


            }
            catch (Exception ex)
            {

                throw new Exception($"Exception Error message: {ex.Message}");
            }

        }

        public async Task<GenericResponse<AuthorResponseDto>> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _dbContext.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (author == null)
                {
                    return GenericResponse<AuthorResponseDto>.Fail
                          ($"Story with the Id {id} does not exist", HttpStatusCode.BadRequest);
                }

                var response = _mapper.Map<AuthorResponseDto>(author);
                return GenericResponse<AuthorResponseDto>.Success("Bookmarked stories retrieved successfully.", response);
            }
            catch(Exception ex)
            {
                throw new Exception($"Exception Error Message: {ex.Message}");
            }
        }


        public async Task<GenericResponse<CommentResponseDto>> DisapproveCommentByAuthor(ApproveByAuthorDto approveRequest)
        {
            try
            {
                var comment = await _dbContext.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == approveRequest.CommentId);
                if (comment == null)
                {
                    return GenericResponse<CommentResponseDto>.Fail
                          ($"Story with the Id {approveRequest.CommentId} does not exist", HttpStatusCode.BadRequest);
                }
                if (comment.IsApproved == true)
                {
                    var roleName = "Author";
                    var adminName = await _roleManager.FindByNameAsync(roleName);
                    //var admin = await _roleManager.GetRoleIdAsync(adminName);
                    var adminId = adminName.Id;
                    if (Guid.TryParse(adminId, out Guid adminIdCheck))
                    {
                        if (approveRequest.RoleId != adminIdCheck)
                        {
                            return GenericResponse<CommentResponseDto>.Fail
                              ($"User {approveRequest.RoleId} is not authorized to approve a comment", HttpStatusCode.BadRequest);
                        }
                    }

                    comment.CreatedAt = DateTime.Now;
                    comment.AdminId = null;
                    comment.AuthorId = approveRequest.AuthorId;
                    comment.IsApproved = false;

                    _dbContext.Update(comment);

                    await _dbContext.SaveChangesAsync();

                    var response = _mapper.Map<CommentResponseDto>(comment);
                    return GenericResponse<CommentResponseDto>.Success
                          ("Story has been approved Successfully", response);
                }
                return GenericResponse<CommentResponseDto>.Fail
                              ($"Comment {approveRequest.CommentId} has already been disapproved", HttpStatusCode.BadRequest);


            }
            catch (Exception ex)
            {

                throw new Exception($"Exception Error message: {ex.Message}");
            }

        }
    }
}
