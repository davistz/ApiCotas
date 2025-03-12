using ApiCotas.Cotas;

namespace ApiCotas;

public record CotaRequest(double numeroCota, decimal valor, StatusCota status);

