
using System;
using System.Security.Claims;
using WebApplication1.Records;
using WebApplication1.Identity;

using System.Collections.Generic;

namespace WebApplication1.Services;

public class IdentityService
{
    public IdentityService(play_testingContext context)
    {
        _context = context;
    }

    private readonly play_testingContext _context;
    private readonly object _contextLock = new();
    
    
    public List<Claim> GetUserClaims(string userId)
    {
        List<Claim> claims = new();
        if (!int.TryParse(userId, out int uid))
        {
            return claims;
        }
        lock (_contextLock)
        {
            _context.
            foreach (Aspnetuserclaim aspnetuserclaim in _context.Aspnetuserclaims.Where(c => c.UserId == uid))
            {
                claims.Add(new Claim(aspnetuserclaim.Claimtype, aspnetuserclaim.Claimvalue));
            }
            return claims;
        }
    }
} 