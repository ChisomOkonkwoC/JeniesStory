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
    public class StoryService : IStoryService
    {
        private readonly IAuthorService _authorService;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StoryService(IAuthorService authorService, AppDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _authorService = authorService;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GenericResponse<StoryResponseDto>> CreateStoryAsync(StoryRequestDto storyRequest)
        {
            try
            {
                var storyTitle = await _dbContext.Stories.FirstOrDefaultAsync(x => x.Title == storyRequest.Title);
                if (storyTitle != null)
                {
                    return GenericResponse<StoryResponseDto>.Fail
                      ($"Story {storyRequest.Title} already exist", HttpStatusCode.BadRequest);
                }

                var authorId = _authorService.GetAuthorById(storyRequest.AuthorId);
                if(authorId == null)
                {
                    return GenericResponse<StoryResponseDto>.Fail
                      ($"Author {storyRequest.AuthorId} does not exist", HttpStatusCode.BadRequest);
                }
                var roleName = "Author";
                var authorName = await _roleManager.FindByNameAsync(roleName);
                var author = await _roleManager.GetRoleIdAsync(authorName);
                if (Guid.TryParse(author, out Guid authorIdCheck))
                {
                    if (storyRequest.RoleId != authorIdCheck)
                    {
                        return GenericResponse<StoryResponseDto>.Fail
                          ($"User {storyRequest.RoleId} is not authorized to create a story", HttpStatusCode.BadRequest);
                    }
                }


                var newStory = _mapper.Map<Story>(storyRequest);
                newStory.CreatedAt = DateTime.Now;
                newStory.AdminId = null;

                newStory.AuthorId = storyRequest.AuthorId;
                newStory.IsApproved = false;
                _dbContext.Stories.Add(newStory);
                await _dbContext.SaveChangesAsync();

                var response = _mapper.Map<StoryResponseDto>(newStory);
                return GenericResponse<StoryResponseDto>.Success
                      ("Story was Created Successfully", response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Story was not Created:  {ex.Message}");
            }

        }    
              

        

        public async Task<GenericResponse<StoryResponseDto>> PublishStory(ApproveStoryRequestDto approveStoryRequest)
        {
            try
            {
                var story = await _dbContext.Stories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == approveStoryRequest.StoryId);
                if (story == null)
                {
                    return GenericResponse<StoryResponseDto>.Fail
                          ($"Story with the Id {approveStoryRequest.StoryId} does not exist", HttpStatusCode.BadRequest);
                }
                if (story.IsApproved == false)
                {
                    return GenericResponse<StoryResponseDto>.Fail
                          ($"Story with the Id {approveStoryRequest.StoryId} has not been approved", HttpStatusCode.BadRequest);
                }

                var roleName = "Admin";
                var adminName = await _roleManager.FindByNameAsync(roleName);
                //var admin = await _roleManager.GetRoleIdAsync(adminName);
                var adminId = adminName.Id;
                if (Guid.TryParse(adminId, out Guid adminIdCheck))
                {
                    if (approveStoryRequest.RoleId != adminIdCheck)
                    {
                        return GenericResponse<StoryResponseDto>.Fail
                          ($"User {approveStoryRequest.RoleId} is not authorized to publish a story", HttpStatusCode.BadRequest);
                    }
                }

                //var approvedStory = _mapper.Map<Story>(approveStoryRequest);

                story.PublishDate = DateTime.Now;
                story.AdminId = approveStoryRequest.AdminId;
                story.IsPublished = true;

               // _dbContext.Add(story);

                await _dbContext.SaveChangesAsync();

                var response = _mapper.Map<StoryResponseDto>(story);
                return GenericResponse<StoryResponseDto>.Success
                      ("Story has been published Successfully", response);
            }
            catch (Exception ex)
            {

                throw new Exception($"Exception Error message: {ex.Message}");
            }
            


        }

        public async Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllStoriesAsync()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<StoryResponseDto>>( await _dbContext.Stories.ToListAsync());
                
                return GenericResponse<IEnumerable<StoryResponseDto>>.Success
                    ("Stories fetched successfully", result);
            }
            catch (Exception ex)
            {

                throw new Exception($"Get Stories Error: {ex.Message}");
            }
        }

        public async Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllPublishedStoriesAsync()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<StoryResponseDto>>(await _dbContext.Stories.Where(x => x.IsPublished == true).ToListAsync());

                return GenericResponse<IEnumerable<StoryResponseDto>>.Success
                    ("Published Stories fetched successfully", result);
            }
            catch (Exception ex)
            {

                throw new Exception($"Get Stories Error: {ex.Message}");
            }
        }

        
    }
}
