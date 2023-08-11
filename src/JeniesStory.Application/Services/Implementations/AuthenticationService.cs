using AutoMapper;
using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Application.Services.Interfaces;
using JeniesStory.Application.Utilities;
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
using System.Transactions;

namespace JeniesStory.Application.Services.Implementations
{
    public class AuthenticationService : IAuthService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _dbContext;

        public AuthenticationService(ITokenGenerator tokenGenerator, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
                                      IMapper mapper, IEmailService emailService, AppDbContext dbContext)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task<GenericResponse<UserResponseDto>> LoginAynsc(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            
            /*var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);
            if(author == null)
            {
                author.Id = Guid.Parse("default");
            }*/

            if (user == null)
            {
                return GenericResponse<UserResponseDto>.Fail
                    ("Email does not exist", HttpStatusCode.BadRequest);
            }

            if (!user.Status)
            {
                return GenericResponse<UserResponseDto>.Fail("This user has been deactivated", HttpStatusCode.Forbidden);
            }
            if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return GenericResponse<UserResponseDto>.Fail
                    ("Invalid password", HttpStatusCode.BadRequest);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return GenericResponse<UserResponseDto>.Fail
                    ("Email not confirmed", HttpStatusCode.BadRequest);
            }

            var name = await _userManager.GetRolesAsync(user);
            var roleId = "";
            foreach(var role in name)
            {
                var roleName = await _roleManager.FindByNameAsync(role);
                roleId += await _roleManager.GetRoleIdAsync(roleName);


            }
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            var token = await _tokenGenerator.GenerateTokenAsync(user, refreshToken.ToString(), roleId);
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);
            var result = _mapper.Map<UserResponseDto>(user);
            result.Token = token;
            return GenericResponse<UserResponseDto>.Success("Login was Successful", result);
        }

        public async Task<GenericResponse<string>> RegisterAsync(RegistrationRequestDto registrationRequest)
        {
            var userEmail = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (userEmail != null)
            {
                return GenericResponse<string>.Fail
                  ("Email already taken", HttpStatusCode.BadRequest);
            }
            var user = _mapper.Map<User>(registrationRequest);
            user.UserName = registrationRequest.Email;

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var IsMailSent = await _emailService.SendAConfirmationEmailAsync
                  (user, emailToken, "ConfirmEmail.html", registrationRequest.RedirectUrl);
                if (IsMailSent)
                {
                    return GenericResponse<string>.Success("Registration was successful", null);
                }

                return GenericResponse<string>.Fail("Registration not successful");
            }
            var errorMessages = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description));
            return GenericResponse<string>.Fail(errorMessages, HttpStatusCode.BadRequest);
        }

        public async Task<GenericResponse<UserResponseDto>> EmailConfirmationAsync(ConfirmEmailRequestDto confirmEmailRequest)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailRequest.Email);
            //var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Email == confirmEmailRequest.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    return GenericResponse<UserResponseDto>.Fail
                  ($"Email {confirmEmailRequest.Email} already confirmed", HttpStatusCode.BadRequest);
                }
                var decodedToken = TokenConverter.DecodeToken(confirmEmailRequest.Token);
                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if (result.Succeeded)
                {

                    var refreshToken = _tokenGenerator.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    user.ExpiryTime = DateTime.Now.AddDays(1);
                    await _userManager.UpdateAsync(user);

                    var name = await _userManager.GetRolesAsync(user);
                    var roleId = "";
                    foreach (var role in name)
                    {
                        var roleName = await _roleManager.FindByNameAsync(role);
                        roleId += await _roleManager.GetRoleIdAsync(roleName);


                    }
                    
                    var token = await _tokenGenerator.GenerateTokenAsync(user, roleId, refreshToken);
                    var userResponse = _mapper.Map<UserResponseDto>(user);

                    userResponse.Token = token;

                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return GenericResponse<UserResponseDto>.Success
                     ("Email Confirmation was successful", userResponse);
                }
                return GenericResponse<UserResponseDto>.Fail
                ("Email Confirmation token has expired", HttpStatusCode.ResetContent);
            }
            return GenericResponse<UserResponseDto>.Fail
                ("Email not found", HttpStatusCode.NotFound);
        }

        public async Task<GenericResponse<string>> ResendConfirmMailAsync(ResendConfirmationEmailDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return GenericResponse<string>.Fail
                  ("Email cannot be found", HttpStatusCode.NotFound);
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendAConfirmationEmailAsync(user, token,
              "ConfirmEmail.html", request.RedirectUrl);
            return GenericResponse<string>.Success
                 ($"A mail has been sent to {request.Email}", null);
        }

    }
}
