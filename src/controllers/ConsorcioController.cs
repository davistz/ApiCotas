
using ApiCotas.dtos;
using dataContext; 
using Microsoft.EntityFrameworkCore;

namespace ApiCotas.Cotas;

public static class ConsorcioController
{
    public static void AddRoutesConsorcios(this WebApplication app)
    {
        var rotasConsorcios = app.MapGroup("");

        rotasConsorcios.MapPost("/grupo", async (GrupoRequest request, DataContext context, CancellationToken ct) =>
        {
            var novoConsorcio = new GrupoEntity
            {
                Nome = request.Nome,
                ValorTotal = request.ValorTotal,
                NumeroParticipantes = request.NumeroParticipantes,
                DataCreate = DateTime.UtcNow,
                DataUpdate = DateTime.UtcNow
            };

            await context.Consorcios.AddAsync(novoConsorcio, ct);
            await context.SaveChangesAsync(ct);

            var consorcioRetorno = new ConsorcioDTO(novoConsorcio.Id, novoConsorcio.Nome, novoConsorcio.ValorTotal, novoConsorcio.NumeroParticipantes);
            
            return Results.Created($"/grupo/{novoConsorcio.Id}", novoConsorcio);
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
                    consorcio.NumeroParticipantes,
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
            return Results.Ok(consorcio);
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