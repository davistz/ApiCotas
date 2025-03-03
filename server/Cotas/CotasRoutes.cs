namespace ApiCotas.Cotas;

public static class CotasRoutes
{
    public static void AddRoutesCotas(this WebApplication app)
    {
        app.MapGet("cotas", () => "todas as cotas");
    }
}