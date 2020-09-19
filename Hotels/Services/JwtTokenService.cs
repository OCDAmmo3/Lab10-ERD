﻿using Hotels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public JwtTokenService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        public async Task<string> GetToken(ApplicationUser user, TimeSpan? expiresIn)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);
            if (principal == null) return null;

            var signingKey = GetSecurityKey(configuration);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expiresIn,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),

                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null) throw new InvalidOperationException("JWT:Secret is missing!");

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);
        }
    }
}
