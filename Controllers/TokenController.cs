using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Records;

namespace WebApplication1.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]

public class TokenController : Controller
{
    public TokenController(TokenService ts)
    {
        _ts = ts;
    }

    private readonly TokenService _ts;
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetToken(string userId)
    {

        string token = await _ts.IssueToken(userId);
        if (token == "UNAUTHORIZED")
        {
            return Unauthorized();
        }
        return Json(new Token(token));
    }
    
    [HttpGet("decode/{token}")]
    public async Task<IActionResult> DecodeToken(string token)
    {
        return Json(await _ts.Decrypt(token));
    }
}