using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.Services;

public class TokenService
{
    public TokenService()
    {
    }
    
    public Task<string> IssueToken()
    {
        return Task.Run(() =>
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("12345678901234567890123456789012345678901234567890"));//(_config["Jwt:Key"]));
            SigningCredentials credentials = new (securityKey, SecurityAlgorithms.HmacSha256);
            // Claim[] claims = new[]
            // {
            //     new Claim(ClaimTypes.NameIdentifier,user.Username),
            //     new Claim(ClaimTypes.Role,user.Role)
            // };
            var token = new JwtSecurityToken("me",//(_config["Jwt:Issuer"],
                "me",//_config["Jwt:Audience"],
                //claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        });
    }
}