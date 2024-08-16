using System.Security.Claims;
using Core.Common.DataAccess;

namespace Users.Authorization.Common
{
    public static class UserExtensions
    {
        public static Guid GetGuid(this ClaimsPrincipal user)
        {
            Claim? claim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            
            if (claim == null)
                throw new NotFoundException(nameof(claim));

            return Guid.Parse(claim.Value);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");//todo: hardcode
        }
    }
}
