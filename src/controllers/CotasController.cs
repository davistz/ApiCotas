using ApiCotas.dtos;
using dataContext;
using Microsoft.EntityFrameworkCore;

namespace ApiCotas.Cotas;

public static class CotasController
{
    public static void RoutesCotas(this WebApplication app)
    {
        var rotasCotas = app.MapGroup("");

        rotasCotas.MapPost("/{consorcioId}/cotas", async (string consorcioId, CotaRequest request, DataContext context, CancellationToken ct) =>
        {
            var grupo = await context.Consorcios.FindAsync(consorcioId); 

            if (grupo == null)
            {
                return Results.NotFound("Grupo não encontrado.");
            }

            
            var existingCota = await context.Cotas.AnyAsync(cota => cota.NumeroCota == request.numeroCota, ct);

            if (existingCota)
            {
                return Results.Conflict("Já existe uma cota com este número!");
            }

            
            var novaCota = new CotaEntity(request.numeroCota, request.valor, consorcioId, request.status);
            await context.Cotas.AddAsync(novaCota, ct);
            await context.SaveChangesAsync(ct);

            var cotaRetorno = new CotaDTO(novaCota.Id, novaCota.ConsorcioId, novaCota.NumeroCota, novaCota.Valor);

            return Results.Created($"/grupos/{consorcioId}/cotas/{novaCota.Id}", cotaRetorno);
        });
        
        rotasCotas.MapGet("/cotas", async (DataContext context, CancellationToken ct) =>
        {
            var cotas = await context
                .Cotas
                .Where(cota => cota.Status != StatusCota.Inativa)
                .Select(cota => new CotaDTO(cota.Id, cota.ConsorcioId, cota.NumeroCota, cota.Valor))
                .ToListAsync(ct);
            return Results.Ok(cotas);
        });

        rotasCotas.MapGet("/cotas/{id}", async (string id, DataContext context, CancellationToken ct) =>
        {
            var cota = await context.Cotas
                .SingleOrDefaultAsync(c => c.Id == id, ct);

            if (cota == null)
            {
                return Results.NotFound("Cota não encontrada.");
            }

            return Results.Ok(new CotaDTO(cota.Id, cota.ConsorcioId, cota.NumeroCota, cota.Valor));
        });

        rotasCotas.MapPut("/cotas/{id}", async (String id, CotaRequest request, DataContext context, CancellationToken ct) =>
        {
            var cota = await context.Cotas
                .SingleOrDefaultAsync(cota => cota.Id == id);

            if (cota == null)
            {
                return Results.NotFound();
            }
            
            cota.AtualizarCota(request.numeroCota, request.valor, request.status);
            
            await context.SaveChangesAsync(ct);
            return Results.Ok(new CotaDTO(cota.Id, cota.ConsorcioId, cota.NumeroCota, cota.Valor));
        });

        rotasCotas.MapDelete("/cotas/{id}", async (String id, DataContext context, CancellationToken ct) =>
        {
            var cota = await context.Cotas.SingleOrDefaultAsync(cota => cota.Id == id);
            
            if(cota == null)
                return Results.NotFound();
            
            cota.DesativarCota();
            
            await context.SaveChangesAsync(ct);
            return Results.Ok($"A Cota com o id: '{id}' foi deletada!");
        });

    }
}