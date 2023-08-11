using AutoMapper;
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
    public class BookmarkService : IBookmarkService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BookmarkService(UserManager<User> userManager, AppDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GenericResponse<string>> BookmarkStory(Guid storyId, string userId)
        {
            var existingBookmark = await _dbContext.Bookmarks.FirstOrDefaultAsync(x => x.UserId == userId && x.StoryId == storyId);
            if (existingBookmark == null)
            {
                var bookmarkedStory = new Bookmark
                {
                    StoryId = storyId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _dbContext.Bookmarks.AddAsync(bookmarkedStory);
                await _dbContext.SaveChangesAsync();

                return GenericResponse<string>.Success
                 ($"Story has been approved Successfully", bookmarkedStory.Id.ToString());
            }

            return GenericResponse<string>.Fail("Story is already bookmarked.", HttpStatusCode.BadRequest);


        }

        public async Task<GenericResponse<IEnumerable<Story>>> GetBookmarkedStories(string userId)
        {
            var bookmarkedStories = await _dbContext.Bookmarks
                .Where(b => b.UserId == userId)
                .Select(b => b.Story)
                .ToListAsync();

            return GenericResponse<IEnumerable<Story>>.Success("Bookmarked stories retrieved successfully.", bookmarkedStories);
        }

    }
}
