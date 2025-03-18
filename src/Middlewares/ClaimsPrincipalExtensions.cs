using System.Collections.Generic;
using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static ClaimsPrincipal ToClaimsPrincipal(this IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims);
        return new ClaimsPrincipal(identity);
    }
}