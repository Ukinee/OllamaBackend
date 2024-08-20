using Microsoft.AspNetCore.Identity;

namespace Common.Extensions;

public static class IdentityExtensions
{
    public static string GetErrors(this IdentityResult result)
    {
        return result.Errors.Select(x => x.Description).Aggregate((x, y) => x + ", " + y);
    }
}
