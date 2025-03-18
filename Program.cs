using ApiCotas;
using ApiCotas.Cotas;
using ApiCotas.Users;
using dataContext;
using Microsoft.EntityFrameworkCore;
using ApiCotas.Middlewares; // Adicione o namespace do seu middleware

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<AuthService>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Adicione o middleware de autenticação aqui
app.UseMiddleware<AuthMiddleware>(); // Registre seu middleware aqui

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.MapGet("/token", (AuthService authService) =>
{
    var user = new UserEntity
    {
        Nome = "UsuarioPadrao",
        Id = "testeid"
    };

    var token = authService.Generate(user);
    return Results.Ok(new { Token = token });
});

app.MapPost("/login", async (LoginRequest loginRequest, AuthService authService, DataContext context) =>
{
    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
    
    if (user == null || user.Senha != loginRequest.Senha) 
    {
        return Results.Unauthorized(); 
    }

    var token = authService.Generate(user);
    return Results.Ok(new { Token = token });
});

app.UseHttpsRedirection();

app.RoutesConsorcios();
app.RoutesCotas();
app.RoutesUsers();

app.Run();