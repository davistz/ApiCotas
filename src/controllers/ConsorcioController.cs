
using System.IdentityModel.Tokens.Jwt;
using ApiCotas.dtos;
using dataContext; 
using Microsoft.EntityFrameworkCore;

namespace ApiCotas.Cotas;

public static class ConsorcioController
{
    public static void RoutesConsorcios(this WebApplication app)
    {
        var rotasConsorcios = app.MapGroup("");

        rotasConsorcios.MapPost("/grupo", async (GrupoRequest request, DataContext context, HttpContext httpContext, CancellationToken ct) =>
        {
            var authorization = httpContext.Request.Headers["Authorization"].ToString();
            var token = authorization.StartsWith("Bearer ") ? authorization.Substring("Bearer ".Length).Trim() : null;

            if (token == null)
            {
                return Results.Unauthorized();
            }
            
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId"); 
            var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserName");
            
            if (userIdClaim == null)
            {
                return Results.Unauthorized();
            }
            if (usernameClaim == null)
            {
                return Results.Unauthorized();
            }
            
            var userId = userIdClaim.Value;
            var userName = usernameClaim.Value;
            
            var usuario = await context.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
            if (usuario == null)
            {
                return Results.NotFound("Usuário inexistente para criar o grupo");
            }
            
            var novoConsorcio = new GrupoEntity
            {
                Nome = request.Nome,
                ValorTotal = request.ValorTotal,
                Tempo = request.Tempo,
                NumeroParticipantes = request.NumeroParticipantes,
                DataCreate = DateTime.UtcNow,
                DataUpdate = DateTime.UtcNow,
                CriadorId = userId, 
                CriadorNome = userName 
            };
            
            await context.Consorcios.AddAsync(novoConsorcio, ct);
            await context.SaveChangesAsync(ct);


            var consorcioRetorno = new ConsorcioDTO(novoConsorcio.Id, novoConsorcio.Nome, novoConsorcio.ValorTotal, novoConsorcio.Tempo,
                novoConsorcio.NumeroParticipantes, novoConsorcio.CriadorNome, novoConsorcio.CriadorId);
    
            return Results.Created($"/grupo/{novoConsorcio.Id}", consorcioRetorno);
        });
        
        rotasConsorcios.MapGet("/grupo", async (DataContext context, CancellationToken ct) =>
        {
            var consorcios = await context
                .Consorcios
                .Select(consorcio => new 
                {
                    consorcio.Id,
                    consorcio.Nome,
                    consorcio.ValorTotal,
                    consorcio.Tempo,
                    consorcio.NumeroParticipantes,
                    consorcio.CriadorId,
                    consorcio.CriadorNome,
                    consorcio.DataCreate,
                    consorcio.DataUpdate
                })
                .ToListAsync(ct);

            return Results.Ok(consorcios);
        });

        rotasConsorcios.MapPut("/grupo/{id}", async (string id, GrupoRequest request, DataContext context, CancellationToken ct) =>
        {
            var consorcio = await context.Consorcios
                .SingleOrDefaultAsync(c => c.Id == id);

            if (consorcio == null)
            {
                return Results.NotFound();
            }

            consorcio.Nome = request.Nome;
            consorcio.ValorTotal = request.ValorTotal;
            consorcio.NumeroParticipantes = request.NumeroParticipantes;
            consorcio.DataUpdate = DateTime.UtcNow;

            await context.SaveChangesAsync(ct);
            
            var consorcioRetorno = new 
            {
                consorcio.Id,
                consorcio.Nome,
                consorcio.ValorTotal,
                consorcio.Tempo,
                consorcio.NumeroParticipantes,
                consorcio.DataCreate,
                consorcio.DataUpdate
            };
    
            return Results.Ok(consorcioRetorno);
        });

        rotasConsorcios.MapDelete("/grupo/{id}", async (string id, DataContext context, CancellationToken ct) =>
        {
            var consorcio = await context.Consorcios.SingleOrDefaultAsync(c => c.Id == id);

            if (consorcio == null)
                return Results.NotFound();

            context.Consorcios.Remove(consorcio);
            await context.SaveChangesAsync(ct);
            return Results.Ok($"O Consórcio com o id: '{id}' foi deletado!");
        });
    }
}