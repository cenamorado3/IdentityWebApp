using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using WebApplication1.Interfaces;

namespace WebApplication1.Services;

public class TokenService
{
    public TokenService(IdentityService identityService
                        ,IRubiconService imajeep)
    {
        _is = identityService;
        _beepbeep = imajeep;
    }

    private readonly IdentityService _is;
    private readonly IRubiconService _beepbeep;
    public Task<string> IssueToken(string userId)//this should validate identity before authenticating
    {
        return Task.Run(() =>
        {
            ClaimsIdentity ci = new(_is.GetUserClaims(userId));
            
            if (!ci.HasClaim("admin", "admin"))
            {
                return "Unauthorized";
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("12345678901234567890123456789012345678901234567890"));//(_config["Jwt:Key"]));
            SigningCredentials credentials = new (securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("me",//(_config["Jwt:Issuer"],
                "me",//_config["Jwt:Audience"],
                ci.Claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        });
    }
    public async Task<string> IssueTokenProcedure(string userId)//this should validate identity before authenticating
    {
        DataSet set = await _beepbeep.ExecuteCommand(SpRocket.CallUserByIdProc);
        List<Claim> claims = new();
        foreach (DataTable dt in set.Tables)
        {
            if (dt.Columns.Count == 2)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    claims.Add(new Claim(
                        dr.Field<string>("claimtype") ?? ClaimTypes.Thumbprint,
                        dr.Field<string>("claimvalue") ?? ClaimValueTypes.DateTime));
                }
            }
        }

        ClaimsIdentity ci = new(claims);
        
        if (!ci.HasClaim("admin", "admin"))
        {
            return "Unauthorized";
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes("12345678901234567890123456789012345678901234567890"));//(_config["Jwt:Key"]));
        SigningCredentials credentials = new (securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken("me",//(_config["Jwt:Issuer"],
            "me",//_config["Jwt:Audience"],
            ci.Claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //todo DELETE THIS
    public Task<JwtSecurityToken> Decrypt(string token)
    {
        return Task.Run(() =>
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        });
    }
}