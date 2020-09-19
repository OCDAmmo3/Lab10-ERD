﻿using Hotels.Models;
using Hotels.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtTokenService tokenService;
        public IdentityUserService(UserManager<ApplicationUser> userManager, JwtTokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }
            return null;
        }

        public async Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.Phone,
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
