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
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(UserManager<User> userManager, AppDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<GenericResponse<AdminResponseDto>> CreateAdmin(AdminRequestDto adminRequest, string roleId)
        {
            var admin = await _userManager.FindByEmailAsync(adminRequest.Email);
            if (admin == null)
            {
                return GenericResponse<AdminResponseDto>.Fail
                      ($"User with the email {adminRequest.Email} does not exist", HttpStatusCode.BadRequest);
            }
            var roleName = "Admin";
            var adminName = await _roleManager.FindByNameAsync(roleName);
            var adminId = await _roleManager.FindByIdAsync(roleId);
            if (adminId == null || roleId != adminName.Id)
            {
                return GenericResponse<AdminResponseDto>.Fail
                      ($"The Id {roleId} does not exist or Id mismatch", HttpStatusCode.BadRequest);
            }

            var newAdmin = _mapper.Map<Admin>(adminRequest);
            newAdmin.CreatedAt = DateTime.Now;
            newAdmin.RoleId = Guid.Parse(adminId.Id);
            newAdmin.UserId = adminRequest.UserId;
            _dbContext.Add(newAdmin);
            await _dbContext.SaveChangesAsync();

            var response = _mapper.Map<AdminResponseDto>(newAdmin);
            return GenericResponse<AdminResponseDto>.Success
                  ("Admin was Created Successfully", response);
        }

        public async Task<GenericResponse<StoryResponseDto>> ApproveStory(ApproveStoryRequestDto approveStoryRequest)
        {
            var story = await _dbContext.Stories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == approveStoryRequest.StoryId);
            if (story == null)
            {
                return GenericResponse<StoryResponseDto>.Fail
                      ($"Story with the Id {approveStoryRequest.StoryId} does not exist", HttpStatusCode.BadRequest);
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
                      ($"User {approveStoryRequest.RoleId} is not authorized to approve a story", HttpStatusCode.BadRequest);
                }
            }

            //var approvedStory = _mapper.Map<Story>(approveStoryRequest);

            story.ApprovedDate = DateTime.Now;
            story.AdminId = approveStoryRequest.AdminId;
            story.IsApproved = true;

            _dbContext.Update(story);

            await _dbContext.SaveChangesAsync();

            var response = _mapper.Map<StoryResponseDto>(story);
            return GenericResponse<StoryResponseDto>.Success
                  ("Story has been approved Successfully", response);


        }

        public async Task<GenericResponse<IEnumerable<StoryResponseDto>>> GetAllApprovedStoriesAsync()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<StoryResponseDto>>(await _dbContext.Stories.Where(x => x.IsApproved == true).ToListAsync());

                return GenericResponse<IEnumerable<StoryResponseDto>>.Success
                    ("Approved Stories fetched successfully", result);
            }
            catch (Exception ex)
            {

                throw new Exception($"Get Stories Error: {ex.Message}");
            }
        }

        public async Task<GenericResponse<CommentResponseDto>> ApproveCommentByAdmin(ApproveByAdminDto approveRequest)
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
                    comment.AuthorId = null;
                    comment.AdminId = approveRequest.AdminId;
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
    }
}
