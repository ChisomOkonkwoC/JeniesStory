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
    public class CommentService : ICommentService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CommentService(UserManager<User> userManager, AppDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GenericResponse<CommentResponseDto>> CreateComment(CommentRequestDto commentRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == commentRequest.UserId);
            if (user == null)
            {
                return GenericResponse<CommentResponseDto>.Fail
                  ($"User {commentRequest.UserId} does not exist", HttpStatusCode.BadRequest);
            }

            var story = await _dbContext.Comments.FirstOrDefaultAsync(x => x.StoryId == commentRequest.StoryId);
            if (story == null)
            {
                return GenericResponse<CommentResponseDto>.Fail
                  ($"Story {commentRequest.StoryId} does not exist", HttpStatusCode.BadRequest);
            }

            var newComment = _mapper.Map<Comment>(commentRequest);
            newComment.CreatedAt = DateTime.Now;
            newComment.AdminId = null;
            newComment.AuthorId = null;
            newComment.IsApproved = false;
            _dbContext.Comments.Add(newComment);
            await _dbContext.SaveChangesAsync();

            var response = _mapper.Map<CommentResponseDto>(newComment);
            return GenericResponse<CommentResponseDto>.Success
                  ("Comment was Created Successfully", response);
        }

        public async Task<GenericResponse<IEnumerable<Comment>>> GetCommentsForStory(Guid storyId)
        {
            var commentForStories = await _dbContext.Comments
                .Where(b => b.StoryId == storyId)
                .ToListAsync();

            return GenericResponse<IEnumerable<Comment>>.Success("Bookmarked stories retrieved successfully.", commentForStories);
        }
    }
}
