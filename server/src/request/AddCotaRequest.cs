using ApiCotas.Cotas;

namespace ApiCotas;

public record AddCotaRequest(double numeroCota, decimal valor, StatusCota status);

