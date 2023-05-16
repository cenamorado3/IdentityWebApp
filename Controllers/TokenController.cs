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
    [HttpGet(Name = "GetToken")]
    public async Task<IActionResult> GetToken()
    {
        return Json(new Token(await _ts.IssueToken()));
    }
}