using ApiCotas.Cotas;

namespace ApiCotas.dtos;

public record CotaDTO(String Id,String ConsorcioId, double NumeroCota, decimal Valor);
