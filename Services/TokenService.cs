using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using WebApplication1.Records;

namespace WebApplication1.Services;

public class TokenService
{
    public TokenService(IdentityService identityService)
    {
        _is = identityService;
    }

    private readonly IdentityService _is;
    public Task<string> IssueToken(string userId)//this should validate identity before authenticating
    {
        return Task.Run(() =>
        {
            List<Claim> userClaims= _is.GetUserClaims(userId);
            if (userClaims.Count <= 0)
            {
                return "UNAUTHORIZED";
            }
            
            
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("12345678901234567890123456789012345678901234567890"));//(_config["Jwt:Key"]));
            SigningCredentials credentials = new (securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("me",//(_config["Jwt:Issuer"],
                "me",//_config["Jwt:Audience"],
                userClaims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        });
    }

    //todo DELETE THIS
    public Task<JwtSecurityToken> Decrypt(string token)
    {
        return Task.Run(() =>
        {
            return ((JwtSecurityToken) new JwtSecurityTokenHandler().ReadJwtToken(token));
        });
    }
}