using ApiCotas.dtos;
using dataContext;
using Microsoft.EntityFrameworkCore;

namespace ApiCotas.Cotas;

public static class CotasController
{
    public static void AddRoutesCotas(this WebApplication app)
    {
        var rotasCotas = app.MapGroup("");

        rotasCotas.MapPost("/grupos/cotas", async (CotaRequest request, DataContext context, CancellationToken ct) =>
        {
            var existingCota = await context.Cotas.AnyAsync(cota => cota.NumeroCota == request.numeroCota, ct);

            if (existingCota)
            {
                return Results.Conflict("Já existe uma cota com este número!");
            }
            
            var novaCota = new CotaEntity(request.numeroCota, request.valor, request.status);
            await context.Cotas.AddAsync(novaCota, ct);
            await context.SaveChangesAsync(ct);
            
            var cotaRetorno = new CotaDTO(novaCota.Id, novaCota.ConsorcioId, novaCota.NumeroCota, novaCota.Valor);
            
            return Results.Ok(cotaRetorno);
            
        });
        
        rotasCotas.MapGet("/grupos/cotas", async (DataContext context, CancellationToken ct) =>
        {
            var cotas = await context
                .Cotas
                .Where(cota => cota.Status != StatusCota.Inativa)
                .Select(cota => new CotaDTO(cota.Id, cota.ConsorcioId, cota.NumeroCota, cota.Valor))
                .ToListAsync(ct);
            return Results.Ok(cotas);
        });

        rotasCotas.MapPut("/grupos/cotas/{id}", async (String id, CotaRequest request, DataContext context, CancellationToken ct) =>
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

        rotasCotas.MapDelete("/grupos/cotas/{id}", async (String id, DataContext context, CancellationToken ct) =>
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