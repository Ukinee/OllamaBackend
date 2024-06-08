using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Authorization.Common;

public static class UserExtensions
{
    public static Guid GetGuid(this ClaimsPrincipal user)
    {
        Claim? claim = user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId);
        
        if (claim == null)
            throw new Exception("Claim not found");
        
        return Guid.Parse(claim.Value);
    }
}
