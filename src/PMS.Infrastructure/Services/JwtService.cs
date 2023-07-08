using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Core.Entities;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;



        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public JwtDto CreateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null");

            if (user.Id == Guid.Empty)
                throw new ArgumentException("UserId is Guid.Empty, could not create token");

            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expire = DateTime.UtcNow.AddHours(int.Parse(_configuration.GetSection("JWT:ExpiryMinutes").Value!));

            var token = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: creds,
                expires: expire );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtDto(jwt, expire, user.Role);
        }
    }
}