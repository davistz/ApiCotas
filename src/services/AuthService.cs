using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ApiCotas.Cotas;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    
    private readonly byte[] key = new byte[32];
    private readonly string base64Key;

    public AuthService()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key); 
        }
        base64Key = Convert.ToBase64String(key); 
    }

    public string Generate(UserEntity user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "O usuário não pode ser nulo.");
        }

        if (string.IsNullOrEmpty(user.Nome))
        {
            throw new ArgumentException("O nome do usuário não pode ser nulo ou vazio.", nameof(user.Nome));
        }

        if (user.Id == String.Empty) 
        {
            throw new ArgumentException("O ID do usuário não pode ser um GUID vazio.", nameof(user.Id));
        }

        var handler = new JwtSecurityTokenHandler();
        var signingKey = new SymmetricSecurityKey(key); 
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserName", user.Nome),
                new Claim("UserId", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = signingCredentials
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims(UserEntity user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Nome));

       
        if (!string.IsNullOrEmpty(user.Roles))
        {
            var roles = user.Roles.Split(','); 
            foreach (var role in roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role.Trim()));
            }
        }

        return ci;
    }
    
    public UserEntity ValidateUser(string email, string senha, List<UserEntity> users) 
    {
        
        var user = users.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        return user; 
    }
}