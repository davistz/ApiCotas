using ApiCotas.Cotas;

namespace ApiCotas;

public record UpdateCotaRequest(double numeroCota, decimal valor, StatusCota status);