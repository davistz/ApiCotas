using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiCotas.Cotas;

public class AuthService
{
    private readonly string key; 

    public AuthService(string key)
    {
        this.key = key;
    }

    public string Key => key;
    
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
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
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
}