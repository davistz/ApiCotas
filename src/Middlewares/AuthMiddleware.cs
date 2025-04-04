using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/index.html", StringComparison.OrdinalIgnoreCase) || 
                context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/swagger-ui.css", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/swagger-ui-bundle.js", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/swagger-ui-standalone-preset.js", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/favicon-32x32.png", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/favicon-16x16.png", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/index.css", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/index.js", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase)) 
            {
                await _next(context);
                return;
            }

            
            if (context.Request.Path.StartsWithSegments("/login", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (context.Request.Path.StartsWithSegments("/users", StringComparison.OrdinalIgnoreCase))
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
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authService.Key)),
                    
                    // Desabilita a validação do emissor do token, permitindo que qualquer entidade que assine o token seja aceita.
                    ValidateIssuer = false,
                    // Desabilita a validação da audiência do token, aceitando tokens destinados a qualquer público.
                    ValidateAudience = false,
                    // Define que não há tolerância para desvios de tempo entre o sistema e o token, aumentando a segurança, mas reduzindo a flexibilidade.
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