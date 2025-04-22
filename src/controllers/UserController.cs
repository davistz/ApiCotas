using ApiCotas.Cotas;

namespace ApiCotas.Users;
using dtos;
using dataContext;
using Microsoft.EntityFrameworkCore;

public static class UserController
{
    public static void RoutesUsers(this WebApplication app)
    {
        var userRotas = app.MapGroup("");

        userRotas.MapPost("/users", async (UserRequest request, DataContext context, CancellationToken ct) =>
        {
            var existingUser = await context.Users.AnyAsync(user => user.Email == request.Email, ct);

            if (existingUser)
            {
                return Results.Conflict("Já existe um user com este e-mail!");
            }

            var novoUser = new UserEntity(request.Nome, request.Email, request.Senha);
            await context.Users.AddAsync(novoUser, ct);
            await context.SaveChangesAsync(ct);
            
            var userRetorno = new UserDTO(novoUser.Id, novoUser.Nome, novoUser.Email, novoUser.Senha);
            
            return Results.Ok(userRetorno);
            
        });

        userRotas.MapGet("/users", async (DataContext context, CancellationToken ct) =>
        {
            var users = await context
                .Users
                .Select(user => new UserDTO(user.Id, user.Nome, user.Email, user.Senha))
                .ToListAsync(ct);
            return Results.Ok(users);
        });

        userRotas.MapGet("/users/{id}", async (string id, DataContext context, CancellationToken ct) =>
        {
            var user = await context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDTO(u.Id, u.Nome, u.Email, u.Senha))
                .FirstOrDefaultAsync(ct);

            if (user == null)
            {
                return Results.NotFound("Usuário não encontrado.");
            }

            return Results.Ok(user);
        });


        userRotas.MapPut("/users/{id}", async (String id, UserRequest request, DataContext context, CancellationToken ct) =>
        {
            var user = await context.Users
                .SingleOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return Results.NotFound();
            }
            
            user.AtualizarUser(request.Nome, request.Email, request.Senha);
            
            await context.SaveChangesAsync(ct);
            return Results.Ok(new UserDTO(user.Id, user.Nome, user.Email, user.Senha));
        });
    }
}