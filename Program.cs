using ApiCotas;
using ApiCotas.Cotas;
using ApiCotas.Users;
using dataContext;
using Microsoft.EntityFrameworkCore;
using ApiCotas.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"];
Console.WriteLine($"Chave JWT: {jwtKey}");
if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("A chave JWT não foi configurada. Verifique o appsettings.json.");
}
builder.Services.AddSingleton(new AuthService(jwtKey));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

app.UseMiddleware<AuthMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
        c.RoutePrefix = string.Empty;
    });
}

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