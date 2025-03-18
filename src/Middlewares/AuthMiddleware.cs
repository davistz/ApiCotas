using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCotas.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthService _authService;

        public AuthMiddleware(RequestDelegate next, AuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/auth"))
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

               
                var claimsPrincipal = ValidateToken(token);
                if (claimsPrincipal != null)
                {
                    context.User = claimsPrincipal;
                    await _next(context);
                    return;
                }
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido!");
                return;
            }
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token ausente!");
            return;
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_authService.Base64Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                return jwtToken?.Claims.ToClaimsPrincipal(); 
            }
            catch
            {
                return null;
            }
        }
    }
}