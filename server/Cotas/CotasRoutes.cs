namespace ApiCotas.Cotas;

public static class CotasRoutes
{
    public static void AddRoutesCotas(this WebApplication app)
    {
        app.MapGet("cotas", () => new Cota(
            Guid.NewGuid(), 
            1.0,            
            100.0,         
            StatusCota.Ativa,
            DateTime.Now  
        ));
    }
}