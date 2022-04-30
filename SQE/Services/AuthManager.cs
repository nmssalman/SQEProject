using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SQE.Data;
using SQE.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SQE.Services
{
    public class AuthManager : IAuthManager
    {
        public readonly UserManager<ApiUser> _userManager;
        public readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var siginigCredencials = GetSiginigCredencials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(siginigCredencials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials siginigCredencials, List<Claim> claims)
        {
            var JWTSettings = _configuration.GetSection("JWT");
            var expire = DateTime.Now.AddMinutes(Convert.ToDouble(JWTSettings.GetSection("LifeTime").Value));
            var token = new JwtSecurityToken(
                issuer: JWTSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expire,
                signingCredentials: siginigCredencials
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSiginigCredencials()
        {
            var JWTSettings = _configuration.GetSection("JWT");
            //var Key = Environment.GetEnvironmentVariable("KEY");
            var Key = JWTSettings.GetSection("Key").Value;
            //var Key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginDTO loginUserDTO)
        {
            _user = await _userManager.FindByNameAsync(loginUserDTO.Email);

            return (_user != null && await _userManager.CheckPasswordAsync(_user, loginUserDTO.Password));
        }
    }
}
